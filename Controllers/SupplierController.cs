using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Util;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController<SupplierController>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SupplierController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Supplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierRequest>>> GetSupplier()
        {
            var supplier = await _repositoryWrapper.Supplier.ListSupplier();
            return Ok(supplier);
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierRequest>> GetSupplier(int id)
        {

            var supplier = await _repositoryWrapper.Supplier.FindByIDAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return SupplierToDTO(supplier);
        }

        // PUT: api/Supplier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierRequest supplierRequest)
        {
            if (id != supplierRequest.Id)
            {
                return BadRequest();
            }

            var supplier = await _repositoryWrapper.Supplier.FindByIDAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            supplier.SupplierName = supplierRequest.SupplierName;
            supplier.SupplierAddress = supplierRequest.SupplierAddress;
            supplier.SupplierTypeId = supplierRequest.SupplierTypeId;
            supplier.SupplierPhoto = supplierRequest.SupplierPhoto;

            try
            {
                await _repositoryWrapper.Supplier.UpdateAsync(supplier);

                // ! EventLog
                await _repositoryWrapper.EventLog.Update(supplier);

                FileService.DeleteFileNameOnly("SupplierPhoto", id.ToString());
                FileService.MoveTempFile("SupplierPhoto",
                                         supplier.Id.ToString(),
                                         supplier.SupplierPhoto);
            }
            catch (DbUpdateConcurrencyException) when (!SupplierExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Supplier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierRequest>> CreateSupplier(SupplierRequest supplierRequest)
        {

            var supplier = new Supplier
            {
                SupplierName = supplierRequest.SupplierName,
                RegisterDate = DateTime.Now,
                SupplierAddress = supplierRequest.SupplierAddress,
                SupplierTypeId = supplierRequest.SupplierTypeId,
                SupplierPhoto = supplierRequest.SupplierPhoto,
            };

            await _repositoryWrapper.Supplier.CreateAsync(supplier);

            // ! EventLog
            await _repositoryWrapper.EventLog.Insert(supplier);

            // ? File Upload
            if (supplier.SupplierPhoto != null && supplier.SupplierPhoto != "")
            {
                FileService.MoveTempFile("SupplierPhoto", supplier.Id.ToString(), supplier.SupplierPhoto);

                // ?Multiple File Upload
                // FileService.MoveTempFileDir("SupplierPhoto", supplier.Id.ToString(), supplier.SupplierPhoto);
            }

            return CreatedAtAction(
                nameof(GetSupplier),
                new { id = supplier.Id },
                SupplierToDTO(supplier));
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _repositoryWrapper.Supplier.FindByIDAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.Supplier.DeleteAsync(supplier, true);

            // ! EventLog
            await _repositoryWrapper.EventLog.Delete(supplier);

            // ? Single File Delete
            FileService.DeleteFileNameOnly("SupplierPhoto", id.ToString());

            // ? Multiple File Delete
            // FileService.DeleteDir("SupplierPhoto", id.ToString());


            return NoContent();
        }

        // Search

        [HttpPost("search/{filter}")]
        // public async Task<ActionResult<IEnumerable<SupplierRequest>>> SearchSupplier(string term)
        // {
        //     var empList = await _repositoryWrapper.Supplier.SearchSupplier(term);
        //     return Ok(empList);
        // }

        public async Task<ActionResult<IEnumerable<SupplierSearchPayload>>> SearchSupplierCombo(string filter)
        {
            var cusList = await _repositoryWrapper.Supplier.SearchSupplierCombo(filter);
            return Ok(cusList);
        }


        [HttpPost("searchsupplier")]
        public async Task<ActionResult<IEnumerable<SupplierRequest>>> SearchSupplierMultiple(SupplierResult SearchObj)
        {
            var empList = await _repositoryWrapper.Supplier.SearchSupplierMultiple(SearchObj);
            return Ok(empList);
        }


        private bool SupplierExists(int id)
        {
            return _repositoryWrapper.Supplier.IsExists(id);
        }

        private static SupplierRequest SupplierToDTO(Supplier supplier) =>
            new SupplierRequest
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                RegisterDate = supplier.RegisterDate,
                SupplierAddress = supplier.SupplierAddress,
                SupplierTypeId = supplier.SupplierTypeId,
                SupplierPhoto = supplier.SupplierPhoto,
            };
    }
}
