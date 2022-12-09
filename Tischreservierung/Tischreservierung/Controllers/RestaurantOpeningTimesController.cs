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
    public class RestaurantOpeningTimesController : ControllerBase
    {
        private readonly OnlineReservationContext _context;

        public RestaurantOpeningTimesController(OnlineReservationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantOpeningTime>>> GetRestaurantOpeningTime()
        {
            return await _context.RestaurantOpeningTimes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantOpeningTime>> GetRestaurantOpeningTime(int id)
        {
            var restaurantOpeningTime = await _context.RestaurantOpeningTimes.FindAsync(id);

            if (restaurantOpeningTime == null)
            {
                return NotFound();
            }

            return restaurantOpeningTime;
        }

        [HttpGet("byRestaurant")]
        public async Task<ActionResult<IEnumerable<RestaurantOpeningTime>>> GetRestaurantOpeningTimeByRestaurant(int restaurantId)
        {
            return await _context.RestaurantOpeningTimes.Where(o => o.RestaurantId == restaurantId).ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantOpeningTime(int id, RestaurantOpeningTime restaurantOpeningTime)
        {
            if (id != restaurantOpeningTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurantOpeningTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantOpeningTimeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<RestaurantOpeningTime>> PostRestaurantOpeningTime(RestaurantOpeningTime restaurantOpeningTime)
        {
            _context.RestaurantOpeningTimes.Add(restaurantOpeningTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurantOpeningTime", new { id = restaurantOpeningTime.Id }, restaurantOpeningTime);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantOpeningTime(int id)
        {
            var restaurantOpeningTime = await _context.RestaurantOpeningTimes.FindAsync(id);
            if (restaurantOpeningTime == null)
            {
                return NotFound();
            }

            _context.RestaurantOpeningTimes.Remove(restaurantOpeningTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantOpeningTimeExists(int id)
        {
            return _context.RestaurantOpeningTimes.Any(e => e.Id == id);
        }
    }
}
