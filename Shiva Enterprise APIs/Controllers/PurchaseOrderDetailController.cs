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
    public class PurchaseOrderDetailController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public PurchaseOrderDetailController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetPurchaseOrderDetail")]
        public async Task<ActionResult> GetAllPurchaseOrderDetail()
        {
            var purchaseOrderDetail = await _shivaEnterpriseContext.PurchaseOrderDetails.ToListAsync();

            if (purchaseOrderDetail == null)
                return NotFound();
            return Ok(purchaseOrderDetail);
        }

        [HttpGet]
        [Route("GetProductOrderDetailById")]
        public async Task<ActionResult> GetPurchaseOrderDetailById(Guid purchaseOrderDetailId)
        {
            if (purchaseOrderDetailId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(purchaseOrderDetailId));
            }

            var purchaseOrderDetailData = await _shivaEnterpriseContext.PurchaseOrderDetails.FindAsync(purchaseOrderDetailId);
            if (purchaseOrderDetailData == null)
            {
                return BadRequest("No PurchaseOrderDetail Find");
            }
            return Ok(purchaseOrderDetailData);
        }


        [HttpPost]
        [Route("AddPurchaseOrderDetail")]
        public async Task<ActionResult<PurchaseOrderDetail>> AddPurchaseOrderDetail(PurchaseOrderDetailModel purchaseOrderDetail)
        {
            using (var transaction = _shivaEnterpriseContext.Database.BeginTransaction())
            {
                try
                {
                    if (purchaseOrderDetail is null)
                    {
                        throw new ArgumentNullException(nameof(purchaseOrderDetail));
                    }
                    var purchaseOrderDetailDetail = new PurchaseOrderDetail()
                    {
                        PurchaseOrderId = purchaseOrderDetail.PurchaseOrderId,
                        ProductId = purchaseOrderDetail.ProductId,
                        BrandId = purchaseOrderDetail.BrandId,
                        Quantity = purchaseOrderDetail.Quantity,
                        Discount = purchaseOrderDetail.Discount,
                        UnitPrice = purchaseOrderDetail.UnitPrice,
                        NetTotal = purchaseOrderDetail.NetTotal,
                        Tax_Percentage = purchaseOrderDetail.Tax_Percentage,
                        IsActive = purchaseOrderDetail.IsActive,
                        CreatedBy = purchaseOrderDetail.CreatedBy,
                        CreatedDateTime = purchaseOrderDetail.CreatedDateTime,
                    };
                    _shivaEnterpriseContext.PurchaseOrderDetails.Add(purchaseOrderDetailDetail);
                    await _shivaEnterpriseContext.SaveChangesAsync();
                    transaction.Commit();
                    return Ok("Added Successfully");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }
            }
            
        }

        [HttpPost]
        [Route("DeleteProductType")]
        public async Task<ActionResult<ApiResponseFormat>> DeletePurchaseOrderDetail(Guid purchaseOrderDetailId)
        {
            var deletePurchaseOrderDetail = _shivaEnterpriseContext.PurchaseOrderDetails.Find(purchaseOrderDetailId);
            if (deletePurchaseOrderDetail != null)
            {
                _shivaEnterpriseContext.Entry(deletePurchaseOrderDetail).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditProductType")]
        public async Task<IActionResult> EditProductTypeDetail(Guid id, PurchaseOrderDetail purchaseOrderDetail)
        {
            if (id != purchaseOrderDetail.PurchaseOrderDetailId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(purchaseOrderDetail).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.PurchaseOrderDetails.Any(x => x.PurchaseOrderDetailId == id))
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
