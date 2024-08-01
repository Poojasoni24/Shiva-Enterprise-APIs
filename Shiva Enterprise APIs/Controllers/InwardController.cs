using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Model;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class InwardController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public InwardController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetInwards")]
        public async Task<ActionResult> GetInwards()
        {
            var inwards = await _shivaEnterpriseContext.Inwards.ToListAsync();

            if (inwards == null)
                return NotFound();
            return Ok(inwards);
        }

        [HttpGet]
        [Route("GetInwardsById")]
        public async Task<ActionResult> GetInwardsById(Guid inwardId)
        {
            if (inwardId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(inwardId));
            }

            var inwardDetails = await _shivaEnterpriseContext.Inwards.FindAsync(inwardId);
            if (inwardDetails == null)
            {
                return BadRequest("No Inwards Found");
            }
            return Ok(inwardDetails);
        }


        [HttpPost]
        [Route("AddInwards")]
        public async Task<ActionResult<Inwards>> AddInwards(InwardModel inwardDetails)
        {
            try
            {
                if (inwardDetails is null)
                {
                    throw new ArgumentNullException(nameof(inwardDetails));
                }

                var inwardDetailsObj = new Inwards()
                {
                    PurchaseOrderId = inwardDetails.PurchaseOrder.PurchaseOrderId,
                    VendorId = inwardDetails.Vendor.VendorId,
                    VendorName = inwardDetails.Vendor.VendorName,
                    ReceiptDate = inwardDetails.ReceiptDate,
                    ReceivedBy = inwardDetails.ReceivedBy,
                    ProductId = inwardDetails.Product.ProductId,
                    ProductName = inwardDetails.Product.ProductName,
                    QuantityReceived = inwardDetails.QuantityReceived,
                    UnitOfMeasure = inwardDetails.UnitOfMeasure,
                    BatchNumber = inwardDetails.BatchNumber,
                    QualityCheckStatus = inwardDetails.QualityCheckStatus,
                    QualityCheckRemarks = inwardDetails.QualityCheckRemarks,
                    InvoiceNumber = inwardDetails.InvoiceNumber,
                    InvoiceDate = inwardDetails.InvoiceDate,
                    CostPerUnit = inwardDetails.CostPerUnit,
                    TotalCost = inwardDetails.TotalCost,
                    Remarks = inwardDetails.Remarks,
                    CreatedBy = inwardDetails.CreatedBy,
                    CreatedDate = inwardDetails.CreatedDate,
                    ModifiedBy = inwardDetails.ModifiedBy,
                };

                _shivaEnterpriseContext.Inwards.Add(inwardDetailsObj);
                await _shivaEnterpriseContext.SaveChangesAsync();

                Guid recentlyInsertedId = inwardDetails.InwardId;
                return Ok(recentlyInsertedId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteInwards")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteInwards(Guid inwardsId)
        {
            var deleteInwards = _shivaEnterpriseContext.Inwards.Find(inwardsId);
            if (DeleteInwards != null)
            {
                _shivaEnterpriseContext.Entry(deleteInwards).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditInwards")]
        public async Task<IActionResult> EditInwards(Guid id, Inwards inwards)
        {
            if (id != inwards.InwardId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(inwards).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Inwards.Any(x => x.InwardId == id))
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
