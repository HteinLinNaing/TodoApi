using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoApi.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Employee>> SearchEmployee(string searchTerm)
        {
            return await RepositoryContext.Employees
                        .Where(s => s.EmployeeName.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeResult>> ListEmployee()
        {
            // return await RepositoryContext.Employees
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Employees
            //             .Include(e => e.EmpDepartment)
            //             .OrderBy(s => s.Id).ToListAsync();
            // return await RepositoryContext.Employees
            //             .Select(e => new EmployeeResult{
            //                 Id = e.Id,
            //                 EmployeeName = e.EmployeeName,
            //                 EmployeeAddress = e.EmployeeAddress,
            //                 EmpDepartmentId = e.EmpDepartmentId
            //             })
            //             .OrderBy(s => s.Id).ToListAsync();
            return await RepositoryContext.Employees
                        .Select(e => new EmployeeResult
                        {
                            Id = e.Id,
                            EmployeeName = e.EmployeeName,
                            EmployeeAddress = e.EmployeeAddress,
                            EmpDepartmentId = e.EmpDepartmentId,
                            EmpDepartmentName = e.EmpDepartment!.DeptName
                        })
                        .OrderBy(s => s.Id).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.Employees.Any(e => e.Id == id);
        }

        public Task<IEnumerable<Employee>> SearchEmployeeMultiple(EmployeeResult SearchObj)
        {
            throw new NotImplementedException();
        }

        // Task<IEnumerable<EmployeeResult>> IEmployeeRepository.ListEmployee()
        // {
        //     throw new NotImplementedException();
        // }
    }

}