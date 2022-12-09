using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tischreservierung.Data;
using Tischreservierung.Models.Person;

namespace Tischreservierung.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers() {
            return Ok(await _customerRepository.GetCustomers());
        }

        [HttpGet("{Mail}")]
        public async Task<ActionResult<Customer>> GetCustomerByMail(string mail)
        {
            return Ok(await _customerRepository.GetCustomerByEMail(mail));
        }

        [HttpDelete("{Mail}")]
        public async Task<ActionResult> DeleteCustomer (string mail)
        {
            var customer = await _customerRepository.GetCustomerByEMail(mail);
            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.DeleteCustomer(customer);
            await _customerRepository.Save();
            return NoContent();
        }

        [HttpPost("{Mail}")]
        public async Task<ActionResult> PostCustomer (string mail)
        {
            var customer = await _customerRepository.GetCustomerByEMail(mail);
            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.SetCustomer(customer);
            await _customerRepository.Save();
            return NoContent();
        }
    }
}
