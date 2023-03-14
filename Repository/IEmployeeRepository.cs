using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<IEnumerable<Employee>> SearchEmployee(string searchName);
        Task<IEnumerable<EmployeeResult>> ListEmployee();
        Task<IEnumerable<Employee>> SearchEmployeeMultiple(EmployeeResult SearchObj);
        bool IsExists(long id);
    }
}