using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Helper;
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
        [Route("EditProductOrderDetail")]
        public async Task<IActionResult> EditProductOrderDetail(Guid id, List<PurchaseOrderDetailModel> purchaseOrderDetail)
        {
            try
            {
                foreach (var model in purchaseOrderDetail)
                {
                    var existingItem = _shivaEnterpriseContext.PurchaseOrderDetails.FirstOrDefault(i => i.PurchaseOrderDetailId == model.PurchaseOrderDetailId.Value);
                    if (existingItem == null)
                    {
                        return NotFound();
                    }
                    var entityToUpdate = PurchaseOrderDetailMappingHelper.MapToEntity(model);
                    existingItem.PurchaseOrderId = entityToUpdate.PurchaseOrderId;
                    existingItem.ProductId = entityToUpdate.ProductId;
                    existingItem.BrandId = entityToUpdate.BrandId;
                    existingItem.Quantity = entityToUpdate.Quantity;
                    existingItem.Discount = entityToUpdate.Discount;
                    existingItem.UnitPrice = entityToUpdate.UnitPrice;
                    existingItem.NetTotal = entityToUpdate.NetTotal;
                    existingItem.Tax_Percentage = entityToUpdate.Tax_Percentage;
                    existingItem.IsActive = entityToUpdate.IsActive;
                    existingItem.CreatedBy = entityToUpdate.CreatedBy;
                    existingItem.CreatedDateTime = entityToUpdate.CreatedDateTime;
                    existingItem.ModifiedBy = entityToUpdate.ModifiedBy;
                    existingItem.ModifiedDateTime = entityToUpdate.ModifiedDateTime;

                    _shivaEnterpriseContext.Entry(existingItem).State = EntityState.Modified;

                }
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Updated Successfully");

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

        }

        [HttpGet]
        [Route("GetPurchaseOrderDetailByPurchaseOrderId")]
        public async Task<ActionResult> GetPurchaseOrderDetailByPurchaseOrderId(Guid purchaseOrderId)
        {
            if (purchaseOrderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(purchaseOrderId));
            }

            var purchaseOrderDetails = await _shivaEnterpriseContext.PurchaseOrderDetails
    .Where(s => s.PurchaseOrderId == purchaseOrderId)
    .FirstOrDefaultAsync();

            if (purchaseOrderDetails == null)
            {
                return BadRequest("No PurchaseOrderDetails Find");
            }
            return Ok(purchaseOrderDetails);
        }

    }
}
