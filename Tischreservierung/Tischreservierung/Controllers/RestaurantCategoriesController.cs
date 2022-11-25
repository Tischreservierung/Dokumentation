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
    public class RestaurantCategoriesController : ControllerBase
    {
        private readonly OnlineReservationContext _context;

        public RestaurantCategoriesController(OnlineReservationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantCategory>>> GetTypeOfRestaurants()
        {
            return await _context.RestaurantCategory.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantCategory>> GetRestaurantCategory(string id)
        {
            var restaurantCategory = await _context.RestaurantCategory.FindAsync(id);

            if (restaurantCategory == null)
            {
                return NotFound();
            }

            return restaurantCategory;
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantCategory>> PostRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            _context.RestaurantCategory.Add(restaurantCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RestaurantCategoryExists(restaurantCategory.Category))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRestaurantCategory", new { id = restaurantCategory.Category }, restaurantCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantCategory(string id)
        {
            var restaurantCategory = await _context.RestaurantCategory.FindAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            _context.RestaurantCategory.Remove(restaurantCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantCategoryExists(string id)
        {
            return _context.RestaurantCategory.Any(e => e.Category == id);
        }
    }
}
