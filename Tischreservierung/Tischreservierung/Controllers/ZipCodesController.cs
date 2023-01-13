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
    public class ZipCodesController : ControllerBase
    {
        private readonly IZipCodeRepository _repository;

        public ZipCodesController(IZipCodeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipcodes()
        {
            return Ok(await _repository.GetZipCodes());
        }

        [HttpGet("{zipcode}")]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipCodesByZipCode(string zipcode)
        {
            var zipCode = await _repository.GetByZipCode(zipcode);

            if (zipCode == null)
            {
                return NotFound();
            }

            return Ok(zipCode);
        }

        [HttpGet("byLocation")]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipcodesByLocation(string location)
        {
            return Ok( await _repository.GetByLocation(location));

        }

        [HttpGet("byDistrict")]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipcodesByDistrict(string district)
        {
            return Ok(await _repository.GetByDistrict(district));

        }
       
    }
}
