using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tischreservierung.Data.RestaurantRepo;
using Tischreservierung.Models;

namespace Tischreservierung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantsController(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _repository.GetRestaurants();
            
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _repository.GetRestaurantById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            _repository.InsertRestaurant(restaurant);
            await _repository.Save();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _repository.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _repository.DeleteRestaurant(restaurant);
            await _repository.Save();

            return NoContent();
        }
    }
}
