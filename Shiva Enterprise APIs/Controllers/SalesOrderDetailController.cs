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
    public class SalesOrderDetailController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public SalesOrderDetailController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetSalesOrderDetail")]
        public async Task<ActionResult> GetSalesOrderDetail()
        {
            var salesorderdetail = await _shivaEnterpriseContext.SalesOrderDetails.ToListAsync();

            if (salesorderdetail == null)
                return NotFound();
            return Ok(salesorderdetail);
        }

        [HttpGet]
        [Route("GetSalesOrderDetailById")]
        public async Task<ActionResult> GetSalesOrderDetailById(Guid salesorderdetailId)
        {
            if (salesorderdetailId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(salesorderdetailId));
            }

            var salesorderdetailData = await _shivaEnterpriseContext.SalesOrderDetails.FindAsync(salesorderdetailId);
            if (salesorderdetailData == null)
            {
                return BadRequest("No SalesOrderDetail Find");
            }
            return Ok(salesorderdetailData);
        }


        [HttpPost]
        [Route("AddSalesOrderDetail")]
        public async Task<ActionResult<SalesOrderDetail>> AddSalesOrderDetail(SalesOrderDetailModel salesorderdetail)
        {
            try
            {
                if (salesorderdetail is null)
                {
                    throw new ArgumentNullException(nameof(salesorderdetail));
                }
                var SalesOrderDetail = new SalesOrderDetail()
                {
                    Quantity = salesorderdetail.Quantity,
                    Discount = salesorderdetail.Discount,
                    UnitPrice = salesorderdetail.UnitPrice,
                    NetTotal = salesorderdetail.NetTotal,
                    Tax_Percentage = salesorderdetail.Tax_Percentage,
                    IsActive=salesorderdetail.IsActive,
                    CreatedBy = salesorderdetail.CreatedBy,
                    CreatedDateTime = salesorderdetail.CreatedDateTime,
                };
                _shivaEnterpriseContext.SalesOrderDetails.Add(SalesOrderDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteSalesOrderDetail")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteSalesOrderDetail(Guid salesorderdetailId)
        {
            var deleteSalesOrderDetail = _shivaEnterpriseContext.SalesOrderDetails.Find(salesorderdetailId);
            if (deleteSalesOrderDetail != null)
            {
                _shivaEnterpriseContext.Entry(deleteSalesOrderDetail).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditSalesOrderDetail")]
        public async Task<IActionResult> EditSalesOrderDetail(Guid id, SalesOrderDetail salesorderdetail)
        {
            if (id != salesorderdetail.SalesOrderDetailId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(salesorderdetail).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.SalesOrderDetails.Any(x => x.SalesOrderDetailId == id))
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
