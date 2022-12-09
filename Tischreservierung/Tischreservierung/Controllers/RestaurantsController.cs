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
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantRepository.GetRestaurants();
            
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.InsertRestaurant(restaurant);
            await _restaurantRepository.Save();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _restaurantRepository.DeleteRestaurant(restaurant);
            await _restaurantRepository.Save();

            return NoContent();
        }
    }
}
