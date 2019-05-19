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
    public class TroopTypesController : ODataController
    {
        private readonly DataContext _context;

        public TroopTypesController(DataContext context)
        {
            _context = context;
        }

		// OData service
		[HttpGet]
		[EnableQuery]
		public IEnumerable<TroopType> GetTroopTypes()
		{
			return _context.TroopTypes;
		}

		// GET: api/TroopTypes/5
		[HttpGet("{id}")]
        public async Task<IActionResult> GetTroopType([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var troopType = await _context.TroopTypes.FindAsync(id);

            if (troopType == null)
            {
                return NotFound();
            }

            return Ok(troopType);
        }

        // PUT: api/TroopTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTroopType([FromRoute] long id, [FromBody] TroopType troopType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != troopType.Id)
            {
                return BadRequest();
            }

            _context.Entry(troopType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TroopTypeExists(id))
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

        // POST: api/TroopTypes
        [HttpPost]
        public async Task<IActionResult> PostTroopType([FromBody] TroopType troopType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TroopTypes.Add(troopType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTroopType", new { id = troopType.Id }, troopType);
        }

        // DELETE: api/TroopTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTroopType([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var troopType = await _context.TroopTypes.FindAsync(id);
            if (troopType == null)
            {
                return NotFound();
            }

            _context.TroopTypes.Remove(troopType);
            await _context.SaveChangesAsync();

            return Ok(troopType);
        }

        private bool TroopTypeExists(long id)
        {
            return _context.TroopTypes.Any(e => e.Id == id);
        }
    }
}