using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Model;
using static Shiva_Enterprise_APIs.Entities.Outwards;
namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class OutwardController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public OutwardController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetOutwards")]
        public async Task<ActionResult> GetOutwards()
        {
            var outwards = await _shivaEnterpriseContext.Outwards.ToListAsync();

            if (outwards == null)
                return NotFound();
            return Ok(outwards);
        }

        [HttpGet]
        [Route("GetOutwardsById")]
        public async Task<ActionResult> GetOutwardsById(Guid outwardsId)
        {
            if (outwardsId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(outwardsId));
            }

            var outwardDetails = await _shivaEnterpriseContext.Outwards.FindAsync(outwardsId);
            if (outwardDetails == null)
            {
                return BadRequest("No Outwards Found");
            }
            return Ok(outwardDetails);
        }


        [HttpPost]
        [Route("AddOutwards")]
        public async Task<ActionResult<Outwards>> AddOutwards(OutwardModel outwardDetails)
        {
            try
            {
                if (outwardDetails is null)
                {
                    throw new ArgumentNullException(nameof(outwardDetails));
                }

                var outwardDetailsObj = new Outwards()
                {
                    SalesOrderId = outwardDetails.SalesOrderId,
                    CustomerId = outwardDetails.CustomerId,
                    CustomerName = outwardDetails.CustomerName,
                    ShipmentDate = outwardDetails.ShipmentDate,
                    ShippedBy = outwardDetails.ShippedBy,
                    ProductId = outwardDetails.ProductId,
                    ProductName = outwardDetails.ProductName,
                    QuantityShipped = outwardDetails.QuantityShipped,
                    UnitOfMeasure = outwardDetails.UnitOfMeasure,
                    BatchNumber = outwardDetails.BatchNumber,
                    CarrierId = outwardDetails.CarrierId,
                    CarrierName = outwardDetails.CarrierName,
                    TrackingNumber = outwardDetails.TrackingNumber,
                    ShippingMethod = outwardDetails.ShippingMethod,
                    DeliveryDate = outwardDetails.DeliveryDate,
                    DeliveryAddress = outwardDetails.DeliveryAddress,
                    InvoiceDate = outwardDetails.InvoiceDate,
                    CostPerUnit = outwardDetails.CostPerUnit,
                    TotalCost = outwardDetails.TotalCost,
                    Remarks = outwardDetails.Remarks,
                    CreatedBy = outwardDetails.CreatedBy,
                    CreatedDate = outwardDetails.CreatedDate,
                    ModifiedBy = outwardDetails.ModifiedBy,
                    Currency = outwardDetails.Currency
                };

                _shivaEnterpriseContext.Outwards.Add(outwardDetailsObj);
                await _shivaEnterpriseContext.SaveChangesAsync();

                Guid recentlyInsertedId = outwardDetails.OutwardId;
                return Ok(recentlyInsertedId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteOutwards")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteOutwards(Guid outwardsId)
        {
            var deleteOutwards = _shivaEnterpriseContext.Outwards.Find(outwardsId);
            if (deleteOutwards != null)
            {
                _shivaEnterpriseContext.Entry(deleteOutwards).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditOutwards")]
        public async Task<IActionResult> EditOutwards(Guid id, OutwardModel outwards)
        {
            try
            {
                var existingItem = _shivaEnterpriseContext.Outwards.FirstOrDefault(i => i.OutwardId == id);
                if (existingItem == null)
                {
                    return NotFound();
                }
                var entityToUpdate = MappingHelper.MapToEntity(outwards);

                existingItem.SalesOrderId = entityToUpdate.SalesOrderId;
                existingItem.CustomerId = entityToUpdate.CustomerId;
                existingItem.CustomerName = entityToUpdate.CustomerName;
                existingItem.ShipmentDate = entityToUpdate.ShipmentDate;
                existingItem.ShippedBy = entityToUpdate.ShippedBy;
                existingItem.ProductId = entityToUpdate.ProductId;
                existingItem.ProductName = entityToUpdate.ProductName;
                existingItem.QuantityShipped = entityToUpdate.QuantityShipped;
                existingItem.UnitOfMeasure = entityToUpdate.UnitOfMeasure;
                existingItem.BatchNumber = entityToUpdate.BatchNumber;
                existingItem.CarrierId = entityToUpdate.CarrierId;
                existingItem.CarrierName = entityToUpdate.CarrierName;
                existingItem.TrackingNumber = entityToUpdate.TrackingNumber;
                existingItem.ShippingMethod = entityToUpdate.ShippingMethod;
                existingItem.DeliveryDate = entityToUpdate.DeliveryDate;
                existingItem.DeliveryAddress = entityToUpdate.DeliveryAddress;
                existingItem.InvoiceDate = entityToUpdate.InvoiceDate;
                existingItem.CostPerUnit = entityToUpdate.CostPerUnit;
                existingItem.TotalCost = entityToUpdate.TotalCost;
                existingItem.Remarks = entityToUpdate.Remarks;
                existingItem.CreatedBy = entityToUpdate.CreatedBy;
                existingItem.CreatedDate = entityToUpdate.CreatedDate;
                existingItem.ModifiedBy = entityToUpdate.ModifiedBy;
                existingItem.Currency = entityToUpdate.Currency;

                _shivaEnterpriseContext.Entry(existingItem).State = EntityState.Modified;
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok(existingItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Outwards.Any(x => x.OutwardId == id))
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
        [Route("GetCustomerFromSaleOrderId")]
        public async Task<ActionResult> GetCustomerFromSaleOrderId(Guid saleOrderId)
        {
            if (saleOrderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(saleOrderId));
            }

            var customerIds = await _shivaEnterpriseContext.SalesOrders.Where(p => p.SalesOrderId == saleOrderId).Select(p => p.CustomerId).ToListAsync();
            var customers = await _shivaEnterpriseContext.Customers.Where(p => customerIds.Contains(p.CustomerId)).ToListAsync();

            if (customers == null)
            {
                return BadRequest("No Product Find");
            }
            return Ok(customers);
        }
    }
}
