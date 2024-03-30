using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Products;
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
        public async Task<ActionResult<PurchaseOrderDetail>> AddPurchaseOrderDetail(List<PurchaseOrderDetailModel> purchaseOrderDetail)
        {
            using (var transaction = _shivaEnterpriseContext.Database.BeginTransaction())
            {
                try
                {
                    List<PurchaseOrderDetail> purchaseOrderDetailEntity = new List<PurchaseOrderDetail>();
                    if (purchaseOrderDetail is null)
                    {
                        throw new ArgumentNullException(nameof(purchaseOrderDetail));
                    }
                    foreach (var poDetail in purchaseOrderDetail)
                    {
                        var poDetailEntity = new PurchaseOrderDetail()
                        {
                            PurchaseOrderId = poDetail.PurchaseOrderId,
                            ProductId = poDetail.ProductId,
                            BrandId = poDetail.BrandId,
                            Quantity = poDetail.Quantity,
                            Discount = poDetail.Discount,
                            UnitPrice = poDetail.UnitPrice,
                            NetTotal = poDetail.NetTotal,
                            Tax_Percentage = poDetail.Tax_Percentage,
                            IsActive = poDetail.IsActive,
                            CreatedBy = poDetail.CreatedBy,
                            CreatedDateTime = poDetail.CreatedDateTime,
                        };
                        purchaseOrderDetailEntity.Add(poDetailEntity);
                    }

                    _shivaEnterpriseContext.PurchaseOrderDetails.AddRange(purchaseOrderDetailEntity);
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
        public async Task<IActionResult> EditProductTypeDetail(Guid id, List<PurchaseOrderDetail> purchaseOrderDetail)
        {
            if ( purchaseOrderDetail.Any(x => x.PurchaseOrderId == id))
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
