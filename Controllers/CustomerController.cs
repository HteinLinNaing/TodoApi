using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Util;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CustomerController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerRequest>>> GetCustomer()
        {
            var customer = await _repositoryWrapper.Customer.ListCustomer();
            return Ok(customer);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRequest>> GetCustomer(int id)
        {

            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return CustomerToDTO(customer);
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerRequest customerRequest)
        {
            if (id != customerRequest.Id)
            {
                return BadRequest();
            }

            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerName = customerRequest.CustomerName;
            customer.CustomerAddress = customerRequest.CustomerAddress;
            customer.CustomerTypeId = customerRequest.CustomerTypeId;
            customer.CustomerPhoto = customerRequest.CustomerPhoto;

            try
            {
                await _repositoryWrapper.Customer.UpdateAsync(customer);

                FileService.DeleteFileNameOnly("CustomerPhoto", id.ToString());
                FileService.MoveTempFile("CustomerPhoto",
                                         customer.Id.ToString(),
                                         customer.CustomerPhoto);
            }
            catch (DbUpdateConcurrencyException) when (!CustomerExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerRequest>> CreateCustomer(CustomerRequest customerRequest)
        {

            var customer = new Customer
            {
                CustomerName = customerRequest.CustomerName,
                RegisterDate = DateTime.Now,
                CustomerAddress = customerRequest.CustomerAddress,
                CustomerTypeId = customerRequest.CustomerTypeId,
                CustomerPhoto = customerRequest.CustomerPhoto,
            };

            await _repositoryWrapper.Customer.CreateAsync(customer);

            // ? File Upload
            if (customer.CustomerPhoto != null && customer.CustomerPhoto != "")
            {
                FileService.MoveTempFile("CustomerPhoto", customer.Id.ToString(), customer.CustomerPhoto);

                // ?Multiple File Upload
                // FileService.MoveTempFileDir("CustomerPhoto", customer.Id.ToString(), customer.CustomerPhoto);
            }

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = customer.Id },
                CustomerToDTO(customer));
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _repositoryWrapper.Customer.FindByIDAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.Customer.DeleteAsync(customer, true);


            // ? Single File Delete
            FileService.DeleteFileNameOnly("CustomerPhoto", id.ToString());

            // ? Multiple File Delete
            // FileService.DeleteDir("CustomerPhoto", id.ToString());


            return NoContent();
        }

        // Search

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<CustomerRequest>>> SearchCustomer(string term)
        {
            var empList = await _repositoryWrapper.Customer.SearchCustomer(term);
            return Ok(empList);
        }

        [HttpPost("searchcustomer")]
        public async Task<ActionResult<IEnumerable<CustomerRequest>>> SearchCustomerMultiple(CustomerResult SearchObj)
        {
            var empList = await _repositoryWrapper.Customer.SearchCustomerMultiple(SearchObj);
            return Ok(empList);
        }


        private bool CustomerExists(int id)
        {
            return _repositoryWrapper.Customer.IsExists(id);
        }

        private static CustomerRequest CustomerToDTO(Customer customer) =>
            new CustomerRequest
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                RegisterDate = customer.RegisterDate,
                CustomerAddress = customer.CustomerAddress,
                CustomerTypeId = customer.CustomerTypeId,
                CustomerPhoto = customer.CustomerPhoto,
            };
    }
}
