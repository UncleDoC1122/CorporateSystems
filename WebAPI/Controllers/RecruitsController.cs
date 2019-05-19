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
    public class RecruitsController : ODataController
    {
        private readonly DataContext _context;

        public RecruitsController(DataContext context)
        {
            _context = context;
        }

		// OData service
		[HttpGet]
		[EnableQuery]
		public IEnumerable<Recruit> GetRecruits()
		{
			return _context.Recruits;
		}

		// GET: api/Recruits/5
		[HttpGet("{id}")]
        public async Task<IActionResult> GetRecruit([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recruit = await _context.Recruits.FindAsync(id);

            if (recruit == null)
            {
                return NotFound();
            }

            return Ok(recruit);
        }

        // PUT: api/Recruits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecruit([FromRoute] long id, [FromBody] Recruit recruit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recruit.Id)
            {
                return BadRequest();
            }

            _context.Entry(recruit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecruitExists(id))
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

        // POST: api/Recruits
        [HttpPost]
        public async Task<IActionResult> PostRecruit([FromBody] Recruit recruit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Recruits.Add(recruit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecruit", new { id = recruit.Id }, recruit);
        }

        // DELETE: api/Recruits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruit([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recruit = await _context.Recruits.FindAsync(id);
            if (recruit == null)
            {
                return NotFound();
            }

            _context.Recruits.Remove(recruit);
            await _context.SaveChangesAsync();

            return Ok(recruit);
        }

        private bool RecruitExists(long id)
        {
            return _context.Recruits.Any(e => e.Id == id);
        }
    }
}