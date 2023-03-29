using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ISupplierTypeRepository : IRepositoryBase<SupplierType>
    {
        // Task<IEnumerable<Supplier>> SearchSupplier(string searchName);
        // Task<List<SupplierSearchPayload>> SearchSupplierCombo(string filter);
        // Task<IEnumerable<Supplier>> SearchSupplierMultiple(SupplierResult SearchObj);
        Task<IEnumerable<SupplierType>> ListSupplierType { get; }

        bool IsExists(long id);
    }
}