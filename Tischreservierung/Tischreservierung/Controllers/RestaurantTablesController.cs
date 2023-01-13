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
    public class RestaurantTablesController : ControllerBase
    {
        private readonly IRestaurantTableRepository _repository;

        public RestaurantTablesController(IRestaurantTableRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetRestaurantTables()
        {
            var table = await _repository.GetRestaurantTables();

            return Ok(table);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetRestaurantTable(int id)
        {
            var restaurant = await _repository.GetRestaurantTableById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpGet("/byRestaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<RestaurantTable>>> GetRestaurantTablesByRestaurant(int restaurantId)
        {
            var table = await _repository.GetRestaurantTablesByRestaurant(restaurantId);

            return Ok(table);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantTable>> PostRestaurantTable(RestaurantTable restaurantTable)
        {
            _repository.InsertRestaurantTable(restaurantTable);
            await _repository.Save();

            return CreatedAtAction("GetRestaurantTable", new { id = restaurantTable.Id }, restaurantTable);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantTable(int id)
        {
            var restaurant = await _repository.GetRestaurantTableById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _repository.DeleteRestaurantTable(restaurant);
            await _repository.Save();

            return NoContent();
        }
    }
}
