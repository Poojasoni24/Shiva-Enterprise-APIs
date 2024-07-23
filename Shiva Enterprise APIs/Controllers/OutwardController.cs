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

            var outwardDetails = await _shivaEnterpriseContext.SalesOrders.FindAsync(outwardsId);
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
                    SalesOrderId = outwardDetails.SalesOrder.SalesOrderId,
                    CustomerId = outwardDetails.Customer.CustomerId,
                    CustomerName = outwardDetails.Customer.CustomerName,
                    ShipmentDate = outwardDetails.ShipmentDate,
                    ShippedBy = outwardDetails.ShippedBy,
                    ProductId = outwardDetails.Product.ProductId,
                    ProductName = outwardDetails.Product.ProductName,
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
            var deleteOutwards = _shivaEnterpriseContext.SalesOrders.Find(outwardsId);
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
        public async Task<IActionResult> EditOutwards(Guid id, Outwards outwards)
        {
            if (id != outwards.OutwardId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(outwards).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.SalesOrders.Any(x => x.SalesOrderId == id))
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
