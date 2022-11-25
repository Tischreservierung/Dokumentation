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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetRestaurantTables()
        {
            return await _context.RestaurantTables.ToListAsync();
        }

        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetRestaurantTablesByRestaurant(int restaurantId)
        {
            return await _context.RestaurantTables.Where(t => t.RestaurantId == restaurantId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantTable>> PostRestaurantTable(RestaurantTable restaurantTable)
        {
            _context.RestaurantTables.Add(restaurantTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantTable", new { id = restaurantTable.Id }, restaurantTable);
        }

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
