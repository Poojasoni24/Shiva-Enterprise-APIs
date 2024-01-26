using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Products;


namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductTypeController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public ProductTypeController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetProductType")]
        public async Task<ActionResult> GetAllProductType()
        {
            var productType = await _shivaEnterpriseContext.productTypes.ToListAsync();

            if (productType == null)
                return NotFound();
            return Ok(productType);
        }

        [HttpGet]
        [Route("GetProductTypeById")]
        public async Task<ActionResult> GetProductTypeById(Guid productTypeId)
        {
            if (productTypeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productTypeId));
            }

            var productTypeData = await _shivaEnterpriseContext.productTypes.FindAsync(productTypeId);
            if (productTypeData == null)
            {
                return BadRequest("No ProductTypes Find");
            }
            return Ok(productTypeData);
        }


        [HttpPost]
        [Route("AddProductType")]
        public async Task<ActionResult<ProductType>> AddProductType(ProductType addProductTypeObj)
        {
            try
            {
                if (addProductTypeObj is null)
                {
                    throw new ArgumentNullException(nameof(addProductTypeObj));
                }
                _shivaEnterpriseContext.productTypes.Add(addProductTypeObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteProductType")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteProductType(Guid productTypeId)
        {
            var deleteProductType = _shivaEnterpriseContext.productTypes.Find(productTypeId);
            if (deleteProductType != null)
            {
                _shivaEnterpriseContext.Entry(deleteProductType).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditProductType")]
        public async Task<IActionResult> EditProductTypeDetail(Guid id, ProductType productType)
        {
            if (id != productType.ProductTypeId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(productType).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.productTypes.Any(x => x.ProductTypeId == id))
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

