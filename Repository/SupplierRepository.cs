using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Dynamic;
using Serilog;

namespace TodoApi.Repositories
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Supplier>> SearchSupplier(string searchTerm)
        {
            return await RepositoryContext.Suppliers
                        .Where(s => s.SupplierName.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<List<SupplierSearchPayload>> SearchSupplierCombo(string filter)
        {
            try
            {
                ExpandoObject queryFilter = new();
                queryFilter.TryAdd("@filter", "%" + filter + "%");
                queryFilter.TryAdd("@filterid", filter);
                queryFilter.TryAdd("@filteraddress", filter);

                var SelectQuery = @"SELECT customer_id AS Id, customer_name AS Name, customer_address AS Address FROM tbl_customer WHERE customer_name LIKE @filter OR customer_id = @filterid OR customer_address = @filteraddress ORDER BY customer_name LIMIT 0, 5";

                List<SupplierSearchPayload> custResult = await RepositoryContext.RunExecuteSelectQuery<SupplierSearchPayload>(SelectQuery, queryFilter);

                return custResult;
            }
            catch (Exception ex)
            {
                Log.Error("GetSupplierCombo fail " + ex.Message);
                return new List<SupplierSearchPayload>();
            }
        }

        public async Task<IEnumerable<SupplierResult>> ListSupplier()
        {
            // return await RepositoryContext.Suppliers
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Suppliers
            //             .Include(e => e.SupplierType)
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Suppliers
            //             .Select(e => new EmployeeResult{
            //                 Id = e.Id,
            //                 EmployeeName = e.EmployeeName,
            //                 EmployeeAddress = e.EmployeeAddress,
            //                 EmpDepartmentId = e.EmpDepartmentId
            //             })
            //             .OrderBy(s => s.Id).ToListAsync();
            return await RepositoryContext.Suppliers
                        .Select(e => new SupplierResult
                        {
                            Id = e.Id,
                            SupplierName = e.SupplierName,
                            RegisterDate = e.RegisterDate,
                            SupplierAddress = e.SupplierAddress,
                            SupplierTypeId = e.SupplierTypeId,
                            SupplierPhoto = e.SupplierPhoto,
                            SupplierType = e.SupplierType!.SupplierTypeName
                        })
                        .OrderBy(s => s.Id).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.Suppliers.Any(e => e.Id == id);
        }

        public Task<IEnumerable<Supplier>> SearchSupplierMultiple(SupplierResult SearchObj)
        {
            throw new NotImplementedException();
        }
    }

}