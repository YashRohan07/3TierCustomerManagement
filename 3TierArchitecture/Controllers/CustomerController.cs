using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace _3TierArchitecture.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly CustomerService customerService = new CustomerService();

        [HttpGet]
        [Route("api/customer/getall")]
        public IHttpActionResult GetAllCustomers()
        {
            var customers = customerService.GetAllCustomers();
            if (customers.Any())
                return Ok(new { Message = "Customers retrieved successfully.", Data = customers });

            return NotFound();
        }

        [HttpGet]
        [Route("api/customer/{id}")]
        public IHttpActionResult GetCustomerById(int id)
        {
            var customer = customerService.GetCustomerById(id);
            if (customer != null)
                return Ok(new { Message = "Customer retrieved successfully.", Data = customer });

            return NotFound();
        }

        [HttpPost]
        [Route("api/customer")]
        public IHttpActionResult AddCustomer([FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest("Invalid customer data.");

            customerService.AddCustomer(customerDto);
            return Ok(new { Message = "Customer added successfully", CustomerId = customerDto.Id });
        }

        [HttpPut]
        [Route("api/customer/{id}")]
        public IHttpActionResult UpdateCustomer(int id, [FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null || id != customerDto.Id)
                return BadRequest("Customer data is invalid.");

            customerService.UpdateCustomer(customerDto);
            return Ok(new { Message = "Customer updated successfully." });
        }

        [HttpDelete]
        [Route("api/customer/{id}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            if (customerService.DeleteCustomer(id))
                return Ok(new { Message = "Customer deleted successfully." });

            return NotFound();
        }

        [HttpGet]
        [Route("api/customer/search")]
        public IHttpActionResult Search([FromUri] string name = null, [FromUri] string phone = null, [FromUri] int? id = null)
        {
            var customers = customerService.SearchCustomers(name, phone, id);
            if (customers.Any())
                return Ok(new { Message = "Customers found.", Data = customers });

            return NotFound();
        }
    }
}

