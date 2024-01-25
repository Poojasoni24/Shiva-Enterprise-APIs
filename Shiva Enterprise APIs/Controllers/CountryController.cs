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
    public class CountryController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;

        public CountryController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }
        
        [HttpGet]
        [Route("GetCountry")]
        public async Task<ActionResult> GetAllCountry()
        {
            var countries = await _shivaEnterpriseContext.Countries.ToListAsync();

            if (countries == null)
                return NotFound();
            return Ok(countries);
        }

        [HttpGet]
        [Route("GetcountryById")]
        public async Task<ActionResult> GetCountryById(Guid countryId)
        {
            if (countryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(countryId));
            }

            var countrydata = await _shivaEnterpriseContext.Countries.FindAsync(countryId);
            if (countrydata == null)
            {
                return BadRequest("No country Find");
            }
            return Ok(countrydata);
        }


        [HttpPost]
        [Route("AddCountry")]
        public async Task<ActionResult<Country>> AddCountry(Country country)
        {
            try
            {               
                _shivaEnterpriseContext.Countries.Add(country);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteCountry")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteCountry (Guid countryId)
        {
            var countryDetail = _shivaEnterpriseContext.Countries.Find(countryId);
            if (countryDetail != null)
            {
                _shivaEnterpriseContext.Entry(countryDetail).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditCountry")]
        public async Task<IActionResult> EditCountryDetail(Guid id, Country country)
        {
            if (id != country.Country_ID)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(country).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Countries.Any(x => x.Country_ID == id))
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
