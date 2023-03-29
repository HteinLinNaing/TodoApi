using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierTypeController : BaseController<SupplierTypeController>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SupplierTypeController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/SupplierType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierType>>> GetSupplierType()
        {
            var customerType = await _repositoryWrapper.SupplierType.FindAllAsync();
            return Ok(customerType);
        }


        private bool SupplierExists(int id)
        {
            return _repositoryWrapper.SupplierType.IsExists(id);
        }

        private static SupplierType SupplierTypeToDTO(SupplierType customerType) =>
            new SupplierType
            {
                SupplierTypeId = customerType.SupplierTypeId,
                SupplierTypeName = customerType.SupplierTypeName,
                SupplierTypeDescription = customerType.SupplierTypeDescription,
            };
    }
}
