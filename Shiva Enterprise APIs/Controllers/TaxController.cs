using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.TaxEntities;
using Shiva_Enterprise_APIs.Model.Tax;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class TaxController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public TaxController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetTax")]
        public async Task<ActionResult> GetAllTax()
        {
            var tax = await _shivaEnterpriseContext.Taxes.ToListAsync();

            if (tax == null)
                return NotFound();
            return Ok(tax);
        }

        [HttpGet]
        [Route("GetTaxById")]
        public async Task<ActionResult> GetTaxById(Guid taxId)
        {
            if (taxId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(taxId));
            }

            var taxData = await _shivaEnterpriseContext.Taxes.FindAsync(taxId);
            if (taxData == null)
            {
                return BadRequest("No Taxdata Find");
            }
            return Ok(taxData);
        }


        [HttpPost]
        [Route("AddTax")]
        public async Task<ActionResult<Tax>> AddTax(TaxModel tax)
        {
            try
            {
                if (tax is null)
                {
                    throw new ArgumentNullException(nameof(tax));
                }
                var TaxDetail = new Tax()
                {
                    TaxCode = tax.TaxCode,
                    TaxName = tax.TaxName,
                    TaxDescription = tax.TaxDescription,
                    IsActive = tax.IsActive,
                    TaxType= tax.TaxType,
                    TaxRate= tax.TaxRate,
                    CreatedBy = tax.CreatedBy,
                    CreatedDateTime = tax.CreatedDateTime,
                };
                _shivaEnterpriseContext.Taxes.Add(TaxDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteTax")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteTax(Guid TaxId)
        {
            var deleteTax = _shivaEnterpriseContext.Taxes.Find(TaxId);
            if (deleteTax != null)
            {
                _shivaEnterpriseContext.Entry(deleteTax).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditTax")]
        public async Task<IActionResult> EditTaxDetail(Guid id, Tax tax)
        {
            if (id != tax.TaxId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(tax).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Taxes.Any(x => x.TaxId == id))
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

