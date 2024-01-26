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
    public class ProductCategoryController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public ProductCategoryController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }
        [HttpGet]
        [Route("GetProductCategory")]
        public async Task<ActionResult> GetAllProductCategory()
        {
            var productCategory = await _shivaEnterpriseContext.productCategories.ToListAsync();

            if (productCategory == null)
                return NotFound();
            return Ok(productCategory);
        }

        [HttpGet]
        [Route("GetProductCategoryById")]
        public async Task<ActionResult> GetProductCategoryById(Guid productCategoryId)
        {
            if (productCategoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productCategoryId));
            }

            var productCategoryData = await _shivaEnterpriseContext.productCategories.FindAsync(productCategoryId);
            if (productCategoryData == null)
            {
                return BadRequest("No ProductCategories Find");
            }
            return Ok(productCategoryData);
        }


        [HttpPost]
        [Route("AddProductCategory")]
        public async Task<ActionResult<ProductCategory>> AddProductCategory(ProductCategory addProductCategoryObj)
        {
            try
            {
                if (addProductCategoryObj is null)
                {
                    throw new ArgumentNullException(nameof(addProductCategoryObj));
                }
                _shivaEnterpriseContext.productCategories.Add(addProductCategoryObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteProductCategory")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteProductCategory(Guid productCategoryId)
        {
            var deleteProductCategory = _shivaEnterpriseContext.productCategories.Find(productCategoryId);
            if (deleteProductCategory != null)
            {
                _shivaEnterpriseContext.Entry(deleteProductCategory).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditProductCategory")]
        public async Task<IActionResult> EditAccountCategoryDetail(Guid id, ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.productCategories.Any(x => x.ProductCategoryId == id))
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

