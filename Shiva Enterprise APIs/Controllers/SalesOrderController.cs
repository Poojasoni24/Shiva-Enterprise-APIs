using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model;


namespace Shiva_Enterprise_APIs.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class SalesOrderController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public SalesOrderController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetSalesOrder")]
        public async Task<ActionResult> GetSalesOrder()
        {
            var salesorder = await _shivaEnterpriseContext.SalesOrders.ToListAsync();

            if (salesorder == null)
                return NotFound();
            return Ok(salesorder);
        }

        [HttpGet]
        [Route("GetSalesOrderById")]
        public async Task<ActionResult> GetSalesOrderById(Guid salesorderId)
        {
            if (salesorderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(salesorderId));
            }

            var salesorderData = await _shivaEnterpriseContext.SalesOrders.FindAsync(salesorderId);
            if (salesorderData == null)
            {
                return BadRequest("No SalesOrder Find");
            }
            return Ok(salesorderData);
        }


        [HttpPost]
        [Route("AddSalesOrder")]
        public async Task<ActionResult<SalesOrder>> AddSalesOrder(SalesOrderModel salesorder)
        {
            try
            {
                if (salesorder is null)
                {
                    throw new ArgumentNullException(nameof(salesorder));
                }
                var SalesOrderDetail = new SalesOrder()
                {
                    CustomerId=salesorder.CustomerId,
                    OrderDate = salesorder.OrderDate,
                    DeliveryDate = salesorder.DeliveryDate,
                    TotalAmount = salesorder.TotalAmount,
                    SaleOrderStatus = salesorder.SaleOrderStatus,
                    Doc_No = salesorder.Doc_No,
                    CreatedBy = salesorder.CreatedBy,
                    CreatedDateTime = salesorder.CreatedDateTime,
                };
                _shivaEnterpriseContext.SalesOrders.Add(SalesOrderDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();

                Guid recentlyInsertedId = SalesOrderDetail.SalesOrderId;
                return Ok(recentlyInsertedId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteSalesOrder")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteSalesOrder(Guid salesorderId)
        {
            var deleteSalesOrder = _shivaEnterpriseContext.SalesOrders.Find(salesorderId);
            if (deleteSalesOrder != null)
            {
                _shivaEnterpriseContext.Entry(deleteSalesOrder).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditSalesOrder")]
        public async Task<IActionResult> EditSalesOrderDetail(Guid id, SalesOrderModel salesorder)
        {
            if (id != salesorder.SalesOrderId)
            {
                return BadRequest();
            }

            // Retrieve the existing SalesOrder from the database
            var existingSalesOrder = await _shivaEnterpriseContext.SalesOrders.FindAsync(id);

            if (existingSalesOrder == null)
            {
                return NotFound();
            }

            // Update the existing SalesOrder with new values
            existingSalesOrder.CustomerId = salesorder.CustomerId;
            existingSalesOrder.OrderDate = salesorder.OrderDate;
            existingSalesOrder.DeliveryDate = salesorder.DeliveryDate;
            existingSalesOrder.TotalAmount = salesorder.TotalAmount;
            existingSalesOrder.SaleOrderStatus = salesorder.SaleOrderStatus;
            existingSalesOrder.Doc_No = salesorder.Doc_No;
            existingSalesOrder.CreatedBy = salesorder.CreatedBy;
            existingSalesOrder.CreatedDateTime = salesorder.CreatedDateTime;

            _shivaEnterpriseContext.Entry(existingSalesOrder).State = EntityState.Modified;

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

