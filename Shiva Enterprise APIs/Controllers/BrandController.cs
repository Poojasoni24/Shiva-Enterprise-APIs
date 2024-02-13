using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Accounts;
using Shiva_Enterprise_APIs.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class BrandController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public BrandController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetBrand")]
        public async Task<ActionResult> GetAllBrand()
        {
            var brand = await _shivaEnterpriseContext.Brands.ToListAsync();

            if (brand == null)
                return NotFound();
            return Ok(brand);
        }

        [HttpGet]
        [Route("GetBrandById")]
        public async Task<ActionResult> GetBrandById(Guid brandId)
        {
            if (brandId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(brandId));
            }

            var brandData = await _shivaEnterpriseContext.Brands.FindAsync(brandId);
            if (brandData == null)
            {
                return BadRequest("No Bank Find");
            }
            return Ok(brandData);
        }


        [HttpPost]
        [Route("AddBrand")]
        public async Task<ActionResult<Brand>> AddBrand(BrandModel brand)
        {
            try
            {
                if (brand is null)
                {
                    throw new ArgumentNullException(nameof(brand));
                }
                var BrandDetail = new Brand()
                {
                    BrandCode = brand.BrandCode,
                    BrandName = brand.BrandName,
                    BrandDescription = brand.BrandDescription,
                    IsActive = brand.IsActive,
                    CreatedBy = brand.CreatedBy,
                    CreatedDateTime = brand.CreatedDateTime,
                };
                _shivaEnterpriseContext.Brands.Add(BrandDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteBrand")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteBrand(Guid brandId)
        {
            var deleteBrand = _shivaEnterpriseContext.Brands.Find(brandId);
            if (deleteBrand != null)
            {
                _shivaEnterpriseContext.Entry(deleteBrand).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditBrand")]
        public async Task<IActionResult> EditBrandDetailAsync(Guid id, Brand brand)
        {
            if (id != brand.BrandId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Brands.Any(x => x.BrandId == id))
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
