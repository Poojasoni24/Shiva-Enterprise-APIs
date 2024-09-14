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
    public class SalesReturnController
        : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public SalesReturnController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetSalesReturn")]
        public async Task<ActionResult> GetSalesReturn()
        {
            var SalesReturn = await _shivaEnterpriseContext.SalesReturns.ToListAsync();

            if (SalesReturn == null)
                return NotFound();
            return Ok(SalesReturn);
        }

        [HttpGet]
        [Route("GetSalesReturnById")]
        public async Task<ActionResult> GetSalesReturnById(Guid SalesReturnId)
        {
            if (SalesReturnId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(SalesReturnId));
            }

            var SalesReturnData = await _shivaEnterpriseContext.SalesReturns.FindAsync(SalesReturnId);
            if (SalesReturnData == null)
            {
                return BadRequest("No SalesReturn Find");
            }
            return Ok(SalesReturnData);
        }


        [HttpPost]
        [Route("AddSalesReturn")]
        public async Task<ActionResult<SalesReturn>> AddSalesReturn(SalesReturnModel salesReturnModel)
        {
            try
            {
                if (salesReturnModel is null)
                {
                    throw new ArgumentNullException(nameof(salesReturnModel));
                }
                //var salesOrderDetail = await _shivaEnterpriseContext.SalesOrderDetails
            //.FirstOrDefaultAsync(s => s.SalesOrderId == salesReturnModel.SalesOrderID && s.ProductId == salesReturnModel.ProductId);

                var salesOrderDetail = await _shivaEnterpriseContext.SalesOrderDetails
.FirstOrDefaultAsync(s => s.SalesOrderId == salesReturnModel.SalesOrderID);

                if (salesOrderDetail == null)
                {
                    return NotFound("Sales Order Detail not found.");
                }

                salesOrderDetail.Quantity -= salesReturnModel.ReturnedQuantity;

                if (salesOrderDetail.Quantity < 0)
                {
                    return BadRequest("Returned quantity exceeds available quantity.");
                }

                // Save the updated SalesOrderDetail record
                _shivaEnterpriseContext.SalesOrderDetails.Update(salesOrderDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();


                var SalesReturnDetail = new SalesReturn()
                {
                    SalesReturnID = salesReturnModel.SalesReturnID,
                    SalesOrderID = salesReturnModel.SalesOrderID,
                    ReturnDate = salesReturnModel.ReturnDate,
                    ReasonForReturn = salesReturnModel.ReasonForReturn,
                    ReturnedQuantity = salesReturnModel.ReturnedQuantity,
                    RestockingFee = salesReturnModel.RestockingFee,
                    Comments = salesReturnModel.Comments
                };
                _shivaEnterpriseContext.SalesReturns.Add(SalesReturnDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();

               
                Guid recentlyInsertedId = SalesReturnDetail.SalesReturnID;
                return Ok(recentlyInsertedId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteSalesReturn")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteSalesReturn(Guid SalesReturnId)
        {
            var deleteSalesReturn = _shivaEnterpriseContext.SalesReturns.Find(SalesReturnId);

            if (deleteSalesReturn != null)
            {
                var salesOrderDetail = await _shivaEnterpriseContext.SalesOrderDetails
.FirstOrDefaultAsync(s => s.SalesOrderId == deleteSalesReturn.SalesOrderID);

                if (salesOrderDetail == null)
                {
                    return NotFound("Sales Order Detail not found.");
                }

                salesOrderDetail.Quantity += deleteSalesReturn.ReturnedQuantity;

                if (salesOrderDetail.Quantity < 0)
                {
                    return BadRequest("Returned quantity exceeds available quantity.");
                }

                // Save the updated SalesOrderDetail record
                _shivaEnterpriseContext.SalesOrderDetails.Update(salesOrderDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();

                _shivaEnterpriseContext.Entry(deleteSalesReturn).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditSalesReturn")]
        public async Task<IActionResult> EditSalesReturnDetail(Guid id, SalesReturnModel SalesReturn)
        {
            if (id != SalesReturn.SalesReturnID)
            {
                return BadRequest();
            }

            // Retrieve the existing entity from the database
            var existingSalesReturn = await _shivaEnterpriseContext.SalesReturns.FindAsync(id);

            if (existingSalesReturn == null)
            {
                return NotFound();
            }

            // Edit sale quantity in sale
            var salesOrderDetail = await _shivaEnterpriseContext.SalesOrderDetails
.FirstOrDefaultAsync(s => s.SalesOrderId == SalesReturn.SalesOrderID);

            if (salesOrderDetail == null)
            {
                return NotFound("Sales Order Detail not found.");
            }

            salesOrderDetail.Quantity += (existingSalesReturn.ReturnedQuantity - SalesReturn.ReturnedQuantity);

            existingSalesReturn.SalesOrderID = SalesReturn.SalesOrderID;
            existingSalesReturn.ReturnDate = SalesReturn.ReturnDate;
            existingSalesReturn.ReasonForReturn = SalesReturn.ReasonForReturn;
            existingSalesReturn.ReturnedQuantity = SalesReturn.ReturnedQuantity;
            existingSalesReturn.RestockingFee = SalesReturn.RestockingFee;
            existingSalesReturn.Comments = SalesReturn.Comments;

            if (salesOrderDetail.Quantity < 0)
            {
                return BadRequest("Returned quantity exceeds available quantity.");
            }

            // Save the updated SalesOrderDetail record
            _shivaEnterpriseContext.SalesOrderDetails.Update(salesOrderDetail);
            await _shivaEnterpriseContext.SaveChangesAsync();
            // Edit sale quantity in sale

            // Mark the entity as modified
            _shivaEnterpriseContext.Entry(existingSalesReturn).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.SalesReturns.Any(x => x.SalesReturnID == id))
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
