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
        private readonly OnlineReservationContext _context;

        public ZipCodesController(OnlineReservationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipcodes()
        {
            return await _context.Zipcodes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ZipCode>> GetZipCode(int id)
        {
            var zipCode = await _context.Zipcodes.FindAsync(id);

            if (zipCode == null)
            {
                return NotFound();
            }

            return zipCode;
        }

        [HttpGet("byLocation")]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipcodesByLocation(string location)
        {
            return await _context.Zipcodes.Where(z => z.Location == location).ToListAsync();
        }

        [HttpGet("byDistrict")]
        public async Task<ActionResult<IEnumerable<ZipCode>>> GetZipcodesByDistrict(string district)
        {
            return await _context.Zipcodes.Where(z => z.District == district).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ZipCode>> PostZipCode(ZipCode zipCode)
        {
            _context.Zipcodes.Add(zipCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZipCode", new { id = zipCode.Id }, zipCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZipCode(int id)
        {
            var zipCode = await _context.Zipcodes.FindAsync(id);
            if (zipCode == null)
            {
                return NotFound();
            }

            _context.Zipcodes.Remove(zipCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZipCodeExists(int id)
        {
            return _context.Zipcodes.Any(e => e.Id == id);
        }
    }
}
