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
                    PurchaseOrderId = inwardDetails.PurchaseOrderId,
                    VendorId = inwardDetails.VendorId,
                    VendorName = inwardDetails.VendorName,
                    ReceiptDate = inwardDetails.ReceiptDate,
                    ReceivedBy = inwardDetails.ReceivedBy,
                    ProductId = inwardDetails.ProductId,
                    ProductName = inwardDetails.ProductName,
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
        public async Task<IActionResult> EditInwards(Guid id, Shiva_Enterprise_APIs.Model.InwardModel inwards)
        {
            if (id != inwards.InwardId)
            {
                return BadRequest();
            }

            try
            {
                var existingItem = _shivaEnterpriseContext.Inwards.FirstOrDefault(i => i.InwardId == id);
                if (existingItem == null)
                {
                    return NotFound();
                }

                existingItem.PurchaseOrderId = inwards.PurchaseOrderId;
                existingItem.VendorId = inwards.VendorId;
                existingItem.VendorName = inwards.VendorName;
                existingItem.ReceiptDate = inwards.ReceiptDate;
                    existingItem.ReceivedBy = inwards.ReceivedBy;
                    existingItem.ProductId = inwards.ProductId;
                    existingItem.ProductName = inwards.ProductName;
                    existingItem.QuantityReceived = inwards.QuantityReceived;
                    existingItem.UnitOfMeasure = inwards.UnitOfMeasure;
                    existingItem.BatchNumber = inwards.BatchNumber;
                    existingItem.QualityCheckStatus = inwards.QualityCheckStatus;
                    existingItem.QualityCheckRemarks = inwards.QualityCheckRemarks;
                    existingItem.InvoiceNumber = inwards.InvoiceNumber;
                    existingItem.InvoiceDate = inwards.InvoiceDate;
                    existingItem.CostPerUnit = inwards.CostPerUnit;
                    existingItem.TotalCost = inwards.TotalCost;
                    existingItem.Remarks = inwards.Remarks;
                    existingItem.CreatedBy = inwards.CreatedBy;
                    existingItem.CreatedDate = inwards.CreatedDate;
                    existingItem.ModifiedBy = inwards.ModifiedBy;


                _shivaEnterpriseContext.Entry(existingItem).State = EntityState.Modified;
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok(existingItem);


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
            catch(Exception ex)
            {

            }

            return Ok();
        }
    }
}
