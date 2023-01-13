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
    public class RestaurantOpeningTimesController : ControllerBase
    {
        private readonly IOpeningTimeRepository _repository;

        public RestaurantOpeningTimesController(IOpeningTimeRepository repository)
        {
            _repository = repository;
        }

       

        [HttpGet("restaurant")]
        public async Task<ActionResult<IEnumerable<RestaurantOpeningTime>>> GetRestaurantOpeningTimeForRestaurant(int id)
        {
            var restaurantOpeningTime = await _repository.GetOpeningTimesByRestaurant(id);

            if (restaurantOpeningTime == null)
            {
                return NotFound();
            }

            return Ok(restaurantOpeningTime);
        }

        [HttpGet("day")]
        public async Task<ActionResult<IEnumerable<RestaurantOpeningTime>>> GetRestaurantOpeningTimeForDay(int day)
        {
            var restaurantOpeningTime = await _repository.GetOpeningTimesByDay(day);

            if (restaurantOpeningTime == null)
            {
                return NotFound();
            }

            return Ok(restaurantOpeningTime);
        }

        [HttpGet("restaurantDay")]
        public async Task<ActionResult<IEnumerable<RestaurantOpeningTime>>> GetRestaurantOpeningTimeForRestaurantAndDay(int id,int day)
        {
            var restaurantOpeningTime = await _repository.GetOpeningTimesByDayAndRestaurant(id, day);

            if (restaurantOpeningTime == null)
            {
                return NotFound();
            }

            return Ok(restaurantOpeningTime);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurantOpeningTime(int id,RestaurantOpeningTime openingTime)
        {
            if (id != openingTime.Id)
            {
                return BadRequest();
            }

            _repository.UpdateOpeningTime(openingTime);

            try
            {
                await _repository.Save();
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
            _repository.InsertRestaurantOpeningTime(restaurantOpeningTime);
            await _repository.Save();

            return CreatedAtAction("GetRestaurantOpeningTime", new { id = restaurantOpeningTime.Id }, restaurantOpeningTime);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurantOpeningTime(int id)
        {
            var restaurantOpeningTime = await _repository.GetOpeningTime(id);
            if (restaurantOpeningTime == null)
            {
                return NotFound();
            }

            _repository.DeleteOpeningTime(restaurantOpeningTime);
            await _repository.Save();

            return NoContent();
        }

        private bool RestaurantOpeningTimeExists(int id)
        {
            return _repository.GetOpeningTime(id) == null ? false : true;
        }
    }
}
