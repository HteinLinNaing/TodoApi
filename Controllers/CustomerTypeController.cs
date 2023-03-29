using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : BaseController<CustomerTypeController>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CustomerTypeController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/CustomerType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerType>>> GetCustomerType()
        {
            var customerType = await _repositoryWrapper.CustomerType.FindAllAsync();
            return Ok(customerType);
        }


        private bool CustomerExists(int id)
        {
            return _repositoryWrapper.CustomerType.IsExists(id);
        }

        private static CustomerType CustomerTypeToDTO(CustomerType customerType) =>
            new CustomerType
            {
                CustomerTypeId = customerType.CustomerTypeId,
                CustomerTypeName = customerType.CustomerTypeName,
                CustomerTypeDescription = customerType.CustomerTypeDescription,
            };
    }
}
