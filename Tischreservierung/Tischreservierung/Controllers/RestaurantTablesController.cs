using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tischreservierung.Data;
using Tischreservierung.Models;

namespace Tischreservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTablesController : ControllerBase
    {
        private readonly OnlineReservationContext _context;

        public RestaurantTablesController(OnlineReservationContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetRestaurantTables()
        {
            return await _context.RestaurantTables.ToListAsync();
        }

        // POST: api/RestaurantTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RestaurantTable>> PostRestaurantTable(RestaurantTable restaurantTable)
        {
            _context.RestaurantTables.Add(restaurantTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantTable", new { id = restaurantTable.Id }, restaurantTable);
        }

        // DELETE: api/RestaurantTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantTable(int id)
        {
            var restaurantTable = await _context.RestaurantTables.FindAsync(id);
            if (restaurantTable == null)
            {
                return NotFound();
            }

            _context.RestaurantTables.Remove(restaurantTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
