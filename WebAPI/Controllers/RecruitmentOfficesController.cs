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
    public class RecruitmentOfficesController : ODataController
    {
        private readonly DataContext _context;

        public RecruitmentOfficesController(DataContext context)
        {
            _context = context;
        }

        // OData service
        [HttpGet]
		[EnableQuery]
        public IEnumerable<RecruitmentOffice> GetRecruitmentOffices()
        {
            return _context.RecruitmentOffices;
        }

        // GET: api/RecruitmentOffices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecruitmentOffice([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recruitmentOffice = await _context.RecruitmentOffices.FindAsync(id);

            if (recruitmentOffice == null)
            {
                return NotFound();
            }

            return Ok(recruitmentOffice);
        }

        // PUT: api/RecruitmentOffices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecruitmentOffice([FromRoute] long id, [FromBody] RecruitmentOffice recruitmentOffice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recruitmentOffice.Id)
            {
                return BadRequest();
            }

            _context.Entry(recruitmentOffice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecruitmentOfficeExists(id))
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

        // POST: api/RecruitmentOffices
        [HttpPost]
        public async Task<IActionResult> PostRecruitmentOffice([FromBody] RecruitmentOffice recruitmentOffice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RecruitmentOffices.Add(recruitmentOffice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecruitmentOffice", new { id = recruitmentOffice.Id }, recruitmentOffice);
        }

        // DELETE: api/RecruitmentOffices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruitmentOffice([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recruitmentOffice = await _context.RecruitmentOffices.FindAsync(id);
            if (recruitmentOffice == null)
            {
                return NotFound();
            }

            _context.RecruitmentOffices.Remove(recruitmentOffice);
            await _context.SaveChangesAsync();

            return Ok(recruitmentOffice);
        }

        private bool RecruitmentOfficeExists(long id)
        {
            return _context.RecruitmentOffices.Any(e => e.Id == id);
        }
    }
}