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
    public class TypeOfRestaurantsController : ControllerBase
    {
        private readonly OnlineReservationContext _context;

        public TypeOfRestaurantsController(OnlineReservationContext context)
        {
            _context = context;
        }

        // GET: api/TypeOfRestaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfRestaurant>>> GetTypeOfRestaurant()
        {
            return await _context.TypeOfRestaurant.ToListAsync();
        }

        // GET: api/TypeOfRestaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfRestaurant>> GetTypeOfRestaurant(string id)
        {
            var typeOfRestaurant = await _context.TypeOfRestaurant.FindAsync(id);

            if (typeOfRestaurant == null)
            {
                return NotFound();
            }

            return typeOfRestaurant;
        }

        // PUT: api/TypeOfRestaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeOfRestaurant(string id, TypeOfRestaurant typeOfRestaurant)
        {
            if (id != typeOfRestaurant.RestaurantType)
            {
                return BadRequest();
            }

            _context.Entry(typeOfRestaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfRestaurantExists(id))
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

        // POST: api/TypeOfRestaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeOfRestaurant>> PostTypeOfRestaurant(TypeOfRestaurant typeOfRestaurant)
        {
            _context.TypeOfRestaurant.Add(typeOfRestaurant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TypeOfRestaurantExists(typeOfRestaurant.RestaurantType))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTypeOfRestaurant", new { id = typeOfRestaurant.RestaurantType }, typeOfRestaurant);
        }

        // DELETE: api/TypeOfRestaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfRestaurant(string id)
        {
            var typeOfRestaurant = await _context.TypeOfRestaurant.FindAsync(id);
            if (typeOfRestaurant == null)
            {
                return NotFound();
            }

            _context.TypeOfRestaurant.Remove(typeOfRestaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeOfRestaurantExists(string id)
        {
            return _context.TypeOfRestaurant.Any(e => e.RestaurantType == id);
        }
    }
}
