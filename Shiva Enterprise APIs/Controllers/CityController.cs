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
    public class CityController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public CityController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetCity")]
        public async Task<ActionResult> GetAllCity()
        {
            var citydetail = await _shivaEnterpriseContext.Cities.ToListAsync();

            if (citydetail == null)
                return NotFound();
            return Ok(citydetail);
        }

        [HttpGet]
        [Route("GetCityById")]
        public async Task<ActionResult> GetCityById(Guid cityId)
        {
            if (cityId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(cityId));
            }

            var cityData = await _shivaEnterpriseContext.Cities.FindAsync(cityId);
            if (cityData == null)
            {
                return BadRequest("No city Find");
            }
            return Ok(cityData);
        }


        [HttpPost]
        [Route("AddCity")]
        public async Task<ActionResult<CityModel>> AddCity(CityModel cityObj)
        {
            try
            {
                if (cityObj is null)
                {
                    throw new ArgumentNullException(nameof(cityObj));
                }
                var cityDetail = new City()
                {
                    City_Code = cityObj.City_Code,
                    City_Name = cityObj.City_Name,
                    State_Id = cityObj.State_Id,
                    IsActive = cityObj.IsActive,
                    CreatedBy = cityObj.CreatedBy,
                    CreatedDateTime = cityObj.CreatedDateTime
                };
                _shivaEnterpriseContext.Cities.Add(cityDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteCity")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteCity(Guid cityId)
        {
            var cityDetail = _shivaEnterpriseContext.Cities.Find(cityId);
            if (cityDetail != null)
            {
                _shivaEnterpriseContext.Entry(cityDetail).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditCity")]
        public async Task<IActionResult> EditCityDetail(Guid id, City city)
        {
            if (id != city.CityId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(city).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Cities.Any(x => x.CityId == id))
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
