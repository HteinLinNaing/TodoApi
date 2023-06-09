using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Util;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController<AdminController>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AdminController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminRequest>>> GetAdmin()
        {
            var admin = await _repositoryWrapper.Admin.ListAdmin();
            return Ok(admin);
        }

        // GET: api/Admin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminRequest>> GetAdmin(int id)
        {

            var admin = await _repositoryWrapper.Admin.FindByIDAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return AdminToDTO(admin);
        }

        //! PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, AdminUpdateRequest adminUpdateRequest)
        {
            if (id != adminUpdateRequest.Id)
            {
                return BadRequest();
            }

            var admin = await _repositoryWrapper.Admin.FindByIDAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            admin.AdminName = adminUpdateRequest.AdminName;
            admin.Email = adminUpdateRequest.Email;
            admin.LoginName = adminUpdateRequest.LoginName;
            admin.IsActive = adminUpdateRequest.IsActive;
            admin.AdminLevelId = adminUpdateRequest.AdminLevelId;
            admin.AdminPhoto = adminUpdateRequest.AdminPhoto;

            try
            {
                try
                {
                    await _repositoryWrapper.Admin.UpdateAsync(admin);

                    // ! EventLog
                    await _repositoryWrapper.EventLog.Update(admin);
                }
                catch (Exception ex)
                {
                    // ! EventLog
                    await _repositoryWrapper.EventLog.Error("Update Fail", ex.Message);
                }

                if (admin.AdminPhoto != null || admin.AdminPhoto == "")
                {
                    FileService.DeleteFileNameOnly("AdminPhoto", id.ToString());
                    FileService.MoveTempFile("AdminPhoto",
                                             admin.Id.ToString(),
                                             admin.AdminPhoto);
                }
            }
            catch (DbUpdateConcurrencyException) when (!AdminExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        //! POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminRequest>> CreateAdmin(AdminRequest adminRequest)
        {

            var admin = new Admin
            {
                AdminName = adminRequest.AdminName,
                Email = adminRequest.Email,
                LoginName = adminRequest.LoginName,
                IsActive = adminRequest.IsActive,
                AdminLevelId = adminRequest.AdminLevelId,
                AdminPhoto = adminRequest.AdminPhoto,
            };

            var password = adminRequest.Password ?? throw new Exception("null");

            // ! Password Hash
            string salt = Util.SaltedHash.GenerateSalt();
            password = Util.SaltedHash.ComputeHash(salt, password.ToString());
            admin.Password = password;
            admin.Salt = salt;

            await _repositoryWrapper.Admin.CreateAsync(admin);

            await _repositoryWrapper.EventLog.Insert(admin);    //! EventLog

            // ? File Upload
            if (admin.AdminPhoto != null && admin.AdminPhoto != "")
            {
                FileService.MoveTempFile("AdminPhoto", admin.Id.ToString(), admin.AdminPhoto);

                // ?Multiple File Upload
                // FileService.MoveTempFileDir("AdminPhoto", admin.Id.ToString(), admin.AdminPhoto);
            }

            return CreatedAtAction(
                nameof(GetAdmin),
                new { id = admin.Id },
                AdminToDTO(admin));
        }

        //! DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _repositoryWrapper.Admin.FindByIDAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            try
            {
                await _repositoryWrapper.Admin.DeleteAsync(admin, true);

                await _repositoryWrapper.EventLog.Delete(admin);
            }
            catch (Exception ex)
            {
                await _repositoryWrapper.EventLog.Error("Delete Failed : ", ex.Message);
            }

            // ? Single File Delete
            FileService.DeleteFileNameOnly("AdminPhoto", id.ToString());

            // ? Multiple File Delete
            // FileService.DeleteDir("AdminPhoto", id.ToString());

            return NoContent();
        }

        //! Search

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<AdminRequest>>> SearchAdmin(string term)
        {
            var empList = await _repositoryWrapper.Admin.SearchAdmin(term);
            return Ok(empList);
        }

        [HttpPost("searchadmin")]
        public async Task<ActionResult<IEnumerable<AdminRequest>>> SearchAdminMultiple(AdminResult SearchObj)
        {
            var empList = await _repositoryWrapper.Admin.SearchAdminMultiple(SearchObj);
            return Ok(empList);
        }


        private bool AdminExists(int id)
        {
            return _repositoryWrapper.Admin.IsExists(id);
        }

        private static AdminRequest AdminToDTO(Admin admin) =>
            new AdminRequest
            {
                Id = admin.Id,
                AdminName = admin.AdminName,
                Email = admin.Email,
                LoginName = admin.LoginName,
                Password = admin.Password,
                IsActive = admin.IsActive,
                AdminLevelId = admin.AdminLevelId,
                AdminPhoto = admin.AdminPhoto,
            };
    }
}
