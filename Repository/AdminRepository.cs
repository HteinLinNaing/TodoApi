using System.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TodoApi.Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Admin>> SearchAdmin(string searchTerm)
        {
            return await RepositoryContext.Admins
                        .Where(s => s.AdminName.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<AdminResult>> ListAdmin()
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
            return await RepositoryContext.Admins
                        .Select(e => new AdminResult
                        {
                            Id = e.Id,
                            AdminName = e.AdminName,
                            Email = e.Email,
                            LoginName = e.LoginName,
                            // Password = e.Password,
                            IsActive = e.IsActive,
                            AdminPhoto = e.AdminPhoto,
                            AdminLevelId = e.AdminLevelId,
                            AdminLevel = e.AdminLevel!.LevelName
                        })
                        .OrderBy(s => s.Id).ToListAsync();
        }


        public bool IsExists(long id)
        {
            return RepositoryContext.Admins.Any(e => e.Id == id);
        }

        public Task<IEnumerable<Admin>> SearchAdminMultiple(AdminResult SearchObj)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Admin>> IAdminRepository.SearchAdminMultiple(AdminResult SearchObj)
        {
            throw new NotImplementedException();
        }
    }
}