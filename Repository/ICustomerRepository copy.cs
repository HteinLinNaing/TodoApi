using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ISupplierRepository : IRepositoryBase<Supplier>
    {
        // Task<IEnumerable<Supplier>> SearchSupplier(string searchName);
        Task<List<SupplierSearchPayload>> SearchSupplierCombo(string filter);
        Task<IEnumerable<Supplier>> SearchSupplierMultiple(SupplierResult SearchObj);
        Task<IEnumerable<SupplierResult>> ListSupplier();
        bool IsExists(long id);
    }
}