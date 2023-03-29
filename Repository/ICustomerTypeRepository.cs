using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ICustomerTypeRepository : IRepositoryBase<CustomerType>
    {
        // Task<IEnumerable<Customer>> SearchCustomer(string searchName);
        // Task<List<CustomerSearchPayload>> SearchCustomerCombo(string filter);
        // Task<IEnumerable<Customer>> SearchCustomerMultiple(CustomerResult SearchObj);
        Task<IEnumerable<CustomerType>> ListCustomerType { get; }

        bool IsExists(long id);
    }
}