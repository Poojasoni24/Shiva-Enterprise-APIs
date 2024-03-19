using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model.Purchase;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public PurchaseOrderController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetPurchaseOrder")]
        public async Task<ActionResult> GetAllPurchaseOrder()
        {
            var purchaseorder = await _shivaEnterpriseContext.PurchaseOrders.ToListAsync();

            if (purchaseorder == null)
                return NotFound();
            return Ok(purchaseorder);
        }

        [HttpGet]
        [Route("GetPurchaseOrderById")]
        public async Task<ActionResult> GetPurchaseOrderById(Guid purchaseorderId)
        {
            if (purchaseorderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(purchaseorderId));
            }

            var purchaseorderData = await _shivaEnterpriseContext.PurchaseOrders.FindAsync(purchaseorderId);
            if (purchaseorderData == null)
            {
                return BadRequest("No PurchaseOrder Find");
            }
            return Ok(purchaseorderData);
        }


        [HttpPost]
        [Route("AddPurchaseOrder")]
        public async Task<ActionResult<PurchaseOrder>> AddPurchaseOrder(PurchaseOrderModel purchaseorder)
        {
            try
            {
                if (purchaseorder is null)
                {
                    throw new ArgumentNullException(nameof(purchaseorder));
                }
                var PurchaseOrderDetail = new PurchaseOrder()
                {
                    VendorID = purchaseorder.VendorID,
                    OrderDate = purchaseorder.OrderDate,
                    DeliveryDate = purchaseorder.DeliveryDate,
                    TotalAmount = purchaseorder.TotalAmount,
                    PurchaseOrderStatus = purchaseorder.PurchaseOrderStatus,
                    Doc_No = purchaseorder.Doc_No,
                    CreatedBy = purchaseorder.CreatedBy,
                    CreatedDateTime = purchaseorder.CreatedDateTime,
                };
                _shivaEnterpriseContext.PurchaseOrders.Add(PurchaseOrderDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeletePurchaseOrder")]
        public async Task<ActionResult<ApiResponseFormat>> DeletePurchaseOrder(Guid purchaseorderId)
        {
            var deletePurchaseOrder = _shivaEnterpriseContext.PurchaseOrders.Find(purchaseorderId);
            if (deletePurchaseOrder != null)
            {
                _shivaEnterpriseContext.Entry(deletePurchaseOrder).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditPurchaseOrder")]
        public async Task<IActionResult> EditVendorDetail(Guid id, PurchaseOrder purchaseorder)
        {
            if (id != purchaseorder.PurchaseOrderId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(purchaseorder).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.PurchaseOrders.Any(x => x.PurchaseOrderId == id))
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



