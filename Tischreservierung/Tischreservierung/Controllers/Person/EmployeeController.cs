using Microsoft.AspNetCore.Mvc;
using Tischreservierung.Data.Person;
using Tischreservierung.Models.Person;

namespace Tischreservierung.Controllers.Person
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetEmployeeById(int id)
        {
            return Ok(await _employeeRepository.GetEmployeeById(id));
        }


        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<List<Customer>>> GetEmployeeByRestaurantId(int id)
        {
            return Ok(await _employeeRepository.GetByRestaurantId(id));
        }

        [HttpPost("{data}")]
        public async Task<ActionResult> PostEmployee(Employee data)
        {
            _employeeRepository.SetEmployee(data);
            await _employeeRepository.Save();

            return CreatedAtAction("GetCustomerById", new { id = data.Id }, data); ;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.DeleteEmployee(employee);
            await _employeeRepository.Save();
            return NoContent();
        }
    }
}
