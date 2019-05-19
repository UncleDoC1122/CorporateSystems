using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Platform.Data;
using Platform.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalComissionResultsController : ODataController
    {
        private readonly DataContext _context;

        public MedicalComissionResultsController(DataContext context)
        {
            _context = context;
        }

		// OData service
		[HttpGet]
		[EnableQuery]
		public IEnumerable<MedicalComissionResults> GetMedicalComissionResults()
		{
			return _context.MedicalComissionResults;
		}

		// GET: api/MedicalComissionResults/5
		[HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalComissionResults([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicalComissionResults = await _context.MedicalComissionResults.FindAsync(id);

            if (medicalComissionResults == null)
            {
                return NotFound();
            }

            return Ok(medicalComissionResults);
        }

        // PUT: api/MedicalComissionResults/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalComissionResults([FromRoute] long id, [FromBody] MedicalComissionResults medicalComissionResults)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicalComissionResults.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicalComissionResults).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalComissionResultsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MedicalComissionResults
        [HttpPost]
        public async Task<IActionResult> PostMedicalComissionResults([FromBody] MedicalComissionResults medicalComissionResults)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MedicalComissionResults.Add(medicalComissionResults);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalComissionResults", new { id = medicalComissionResults.Id }, medicalComissionResults);
        }

        // DELETE: api/MedicalComissionResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalComissionResults([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicalComissionResults = await _context.MedicalComissionResults.FindAsync(id);
            if (medicalComissionResults == null)
            {
                return NotFound();
            }

            _context.MedicalComissionResults.Remove(medicalComissionResults);
            await _context.SaveChangesAsync();

            return Ok(medicalComissionResults);
        }

        private bool MedicalComissionResultsExists(long id)
        {
            return _context.MedicalComissionResults.Any(e => e.Id == id);
        }
    }
}