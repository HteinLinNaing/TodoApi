using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        // Task<IEnumerable<Customer>> SearchCustomer(string searchName);
        Task<List<CustomerSearchPayload>> SearchCustomerCombo(string filter);
        Task<IEnumerable<Customer>> SearchCustomerMultiple(CustomerResult SearchObj);
        Task<IEnumerable<CustomerResult>> ListCustomer();
        bool IsExists(long id);
    }
}