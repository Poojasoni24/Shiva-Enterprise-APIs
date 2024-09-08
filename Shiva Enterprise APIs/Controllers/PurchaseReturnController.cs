using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model;
using Shiva_Enterprise_APIs.Model.Purchase;

namespace Shiva_Enterprise_APIs.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class PurchaseReturnController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;

        public PurchaseReturnController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetPurchaseReturns")]
        public async Task<ActionResult> GetPurchaseReturns()
        {
            var purchaseReturns = await _shivaEnterpriseContext.PurchaseReturn.ToListAsync();
            if (purchaseReturns == null)
                return NotFound();
            return Ok(purchaseReturns);
        }

        [HttpGet]
        [Route("GetPurchaseReturnById")]
        public async Task<ActionResult> GetPurchaseReturnById(Guid purchaseReturnId)
        {
            if (purchaseReturnId == null)
            {
                throw new ArgumentNullException(nameof(purchaseReturnId));
            }

            var purchaseReturn = await _shivaEnterpriseContext.PurchaseReturn.FindAsync(purchaseReturnId);
            //purchaseReturn.PurchaseOrder = await _shivaEnterpriseContext.PurchaseOrders.FindAsync(purchaseReturn.PurchaseOrderId);
            if (purchaseReturn == null)
            {
                return BadRequest("No Purchase Return Find");
            }
            return Ok(purchaseReturn);
        }

        [HttpPost]
        [Route("AddPurchaseReturn")]
        public async Task<ActionResult<PurchaseReturnModel>> AddPurchaseReturn(PurchaseReturnModel purchaseReturnModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var purchaseOrderDetail = await _shivaEnterpriseContext.PurchaseOrderDetails
                .FirstOrDefaultAsync(s => s.PurchaseOrderId == purchaseReturnModel.PurchaseOrderId && s.ProductId == purchaseReturnModel.ProductId);

                purchaseOrderDetail.Quantity -= purchaseReturnModel.ReturnQuantity;

                if (purchaseOrderDetail.Quantity < 0)
                {
                    return BadRequest("Returned quantity exceeds available quantity.");
                }

                _shivaEnterpriseContext.PurchaseOrderDetails.Update(purchaseOrderDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();

                var purchaseReturn = new PurchaseReturn()
                {
                    PurchaseReturnId = purchaseReturnModel.PurchaseReturnId,
                    PurchaseOrderId = purchaseReturnModel.PurchaseOrderId,
                    TotalAmount = purchaseReturnModel.TotalAmount,
                    ReturnDate = purchaseReturnModel.ReturnDate,
                    ReturnReason = purchaseReturnModel.ReturnReason,
                    ReturnQuantity = purchaseReturnModel.ReturnQuantity,
                    CreatedBy = purchaseReturnModel.CreatedBy,
                    CreatedDateTime = purchaseReturnModel.CreatedDateTime,
                    ModifiedBy = purchaseReturnModel.ModifiedBy,
                    ModifiedDateTime = purchaseReturnModel.ModifiedDateTime,
                    //RestockingFee = purchaseReturnModel.R,
                };

                _shivaEnterpriseContext.PurchaseReturn.Add(purchaseReturn);
                await _shivaEnterpriseContext.SaveChangesAsync();

                Guid recentlyInsertedId = purchaseReturn.PurchaseReturnId;
                return Ok(recentlyInsertedId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeletePurchaseReturn")]
        public async Task<ActionResult> DeletePurchaseReturn(Guid purchaseReturnId)
        {
            var deletepurchaseReturn = _shivaEnterpriseContext.PurchaseReturn.Find(purchaseReturnId);
            if (deletepurchaseReturn != null)
            {
                _shivaEnterpriseContext.Entry(deletepurchaseReturn).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }

            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");

        }

        [HttpPut]
        [Route("EditPurchaseReturn")]
        public async Task<IActionResult> EditPurchaseReturn(Guid id, PurchaseReturnModel purchaseReturn)
        {
            if (id != purchaseReturn.PurchaseReturnId)
            {
                return BadRequest();
            }

            var existingPurchaseReturn = await _shivaEnterpriseContext.PurchaseReturn.FindAsync(id);
            if (existingPurchaseReturn == null)
            {
                return NotFound();
            }

            existingPurchaseReturn.PurchaseOrderId = purchaseReturn.PurchaseOrderId;
            existingPurchaseReturn.ReturnDate = purchaseReturn.ReturnDate;
            existingPurchaseReturn.TotalAmount = purchaseReturn.TotalAmount;
            existingPurchaseReturn.ReturnQuantity = purchaseReturn.ReturnQuantity;
            existingPurchaseReturn.ReturnReason = purchaseReturn.ReturnReason;
            existingPurchaseReturn.Status = purchaseReturn.Status;
            existingPurchaseReturn.CreatedBy = purchaseReturn.CreatedBy;
            existingPurchaseReturn.CreatedDateTime = purchaseReturn.CreatedDateTime;
            existingPurchaseReturn.ModifiedBy = purchaseReturn.ModifiedBy;
            existingPurchaseReturn.ModifiedDateTime = purchaseReturn.ModifiedDateTime;
            //existingPurchaseReturn.PurchaseOrder = purchaseReturn.PurchaseOrder;


            _shivaEnterpriseContext.Entry(existingPurchaseReturn).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.PurchaseReturn.Any(e => e.PurchaseReturnId == id))
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
