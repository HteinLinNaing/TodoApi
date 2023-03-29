using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Dynamic;
using Serilog;

namespace TodoApi.Repositories
{
    public class SupplierTypeRepository : RepositoryBase<SupplierType>, ISupplierTypeRepository
    {
        Task<IEnumerable<SupplierType>> ISupplierTypeRepository.ListSupplierType => throw new NotImplementedException();

        public SupplierTypeRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<SupplierType>> ListSupplierType()
        {
            return await RepositoryContext.SupplierTypes
                        .Select(e => new SupplierType
                        {
                            SupplierTypeId = e.SupplierTypeId,
                            SupplierTypeName = e.SupplierTypeName,
                            SupplierTypeDescription = e.SupplierTypeDescription,
                        })
                        .OrderBy(s => s.SupplierTypeId).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.SupplierTypes.Any(e => e.SupplierTypeId == id);
        }
    }

}