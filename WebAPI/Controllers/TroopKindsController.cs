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
    public class TroopKindsController : ODataController
    {
        private readonly DataContext _context;

        public TroopKindsController(DataContext context)
        {
            _context = context;
        }

		// OData service
		[HttpGet]
		[EnableQuery]
		public IEnumerable<TroopKind> GetTroopKinds()
		{
			return _context.TroopKinds;
		}

		// GET: api/TroopKinds/5
		[HttpGet("{id}")]
        public async Task<IActionResult> GetTroopKind([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var troopKind = await _context.TroopKinds.FindAsync(id);

            if (troopKind == null)
            {
                return NotFound();
            }

            return Ok(troopKind);
        }

        // PUT: api/TroopKinds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTroopKind([FromRoute] long id, [FromBody] TroopKind troopKind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != troopKind.Id)
            {
                return BadRequest();
            }

            _context.Entry(troopKind).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TroopKindExists(id))
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

        // POST: api/TroopKinds
        [HttpPost]
        public async Task<IActionResult> PostTroopKind([FromBody] TroopKind troopKind)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TroopKinds.Add(troopKind);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTroopKind", new { id = troopKind.Id }, troopKind);
        }

        // DELETE: api/TroopKinds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTroopKind([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var troopKind = await _context.TroopKinds.FindAsync(id);
            if (troopKind == null)
            {
                return NotFound();
            }

            _context.TroopKinds.Remove(troopKind);
            await _context.SaveChangesAsync();

            return Ok(troopKind);
        }

        private bool TroopKindExists(long id)
        {
            return _context.TroopKinds.Any(e => e.Id == id);
        }
    }
}