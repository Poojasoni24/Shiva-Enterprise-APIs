using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Model.Vendor;


namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class VendorController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public VendorController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetVendor")]
        public async Task<ActionResult> GetAllVendor()
        {
            var vendor = await _shivaEnterpriseContext.Vendors.ToListAsync();

            if (vendor == null)
                return NotFound();
            return Ok(vendor);
        }

        [HttpGet]
        [Route("GetVendorById")]
        public async Task<ActionResult> GetVendorById(Guid vendorId)
        {
            if (vendorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(vendorId));
            }

            var vendorData = await _shivaEnterpriseContext.Vendors.FindAsync(vendorId);
            if (vendorData == null)
            {
                return BadRequest("No Vendor Find");
            }
            return Ok(vendorData);
        }


        [HttpPost]
        [Route("AddVendor")]
        public async Task<ActionResult<Vendor>> AddVendor(VendorModel vendor)
        {
            try
            {
                if (vendor is null)
                {
                    throw new ArgumentNullException(nameof(vendor));
                }
                var VendorDetail = new Vendor()
                {
                    VendorCode = vendor.VendorCode,
                    VendorName = vendor.VendorName,
                    VendorType = vendor.VendorType,
                    VendorAddress = vendor.VendorAddress,
                    Phoneno = vendor.Phoneno,
                    Email = vendor.Email,
                    BankId = vendor.BankId,
                    TaxId = vendor.TaxId,
                    ContractStartDate = vendor.ContractStartDate,
                    ContractEndDate = vendor.ContractEndDate,
                    Remark = vendor.Remark,
                    IsActive = vendor.IsActive,
                    CreatedBy = vendor.CreatedBy,
                    CreatedDateTime = vendor.CreatedDateTime,
                };
                _shivaEnterpriseContext.Vendors.Add(VendorDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteVendor")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteVendor(Guid vendorId)
        {
            var deleteVendor = _shivaEnterpriseContext.Vendors.Find(vendorId);
            if (deleteVendor != null)
            {
                _shivaEnterpriseContext.Entry(deleteVendor).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditVendor")]
        public async Task<IActionResult> EditVendorDetail(Guid id, Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Vendors.Any(x => x.VendorId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
    }
}


