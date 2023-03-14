using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoApi.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Customer>> SearchCustomer(string searchTerm)
        {
            return await RepositoryContext.Customers
                        .Where(s => s.CustomerName.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<CustomerResult>> ListCustomer()
        {
            // return await RepositoryContext.Customers
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Customers
            //             .Include(e => e.CustomerType)
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Customers
            //             .Select(e => new EmployeeResult{
            //                 Id = e.Id,
            //                 EmployeeName = e.EmployeeName,
            //                 EmployeeAddress = e.EmployeeAddress,
            //                 EmpDepartmentId = e.EmpDepartmentId
            //             })
            //             .OrderBy(s => s.Id).ToListAsync();
            return await RepositoryContext.Customers
                        .Select(e => new CustomerResult
                        {
                            Id = e.Id,
                            CustomerName = e.CustomerName,
                            RegisterDate = e.RegisterDate,
                            CustomerAddress = e.CustomerAddress,
                            CustomerTypeId = e.CustomerTypeId,
                            CustomerPhoto = e.CustomerPhoto,
                            CustomerType = e.CustomerType!.CustomerTypeName
                        })
                        .OrderBy(s => s.Id).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.Customers.Any(e => e.Id == id);
        }

        public Task<IEnumerable<Customer>> SearchCustomerMultiple(CustomerResult SearchObj)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Customer>> ICustomerRepository.SearchCustomerMultiple(CustomerResult SearchObj)
        {
            throw new NotImplementedException();
        }
    }

}