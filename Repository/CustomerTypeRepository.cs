using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Dynamic;
using Serilog;

namespace TodoApi.Repositories
{
    public class CustomerTypeRepository : RepositoryBase<CustomerType>, ICustomerTypeRepository
    {
        Task<IEnumerable<CustomerType>> ICustomerTypeRepository.ListCustomerType => throw new NotImplementedException();

        public CustomerTypeRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<CustomerType>> ListCustomerType()
        {
            return await RepositoryContext.CustomerTypes
                        .Select(e => new CustomerType
                        {
                            CustomerTypeId = e.CustomerTypeId,
                            CustomerTypeName = e.CustomerTypeName,
                            CustomerTypeDescription = e.CustomerTypeDescription,
                        })
                        .OrderBy(s => s.CustomerTypeId).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.CustomerTypes.Any(e => e.CustomerTypeId == id);
        }
    }

}