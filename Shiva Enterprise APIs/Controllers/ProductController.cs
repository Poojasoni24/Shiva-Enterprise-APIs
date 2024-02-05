using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Accounts;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Model.Product;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public ProductController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult> GetAllProduct()
        {
            var product = await _shivaEnterpriseContext.products.ToListAsync();

            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductById")]
        public async Task<ActionResult> GetProductById(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            var productData = await _shivaEnterpriseContext.accounts.FindAsync(productId);
            if (productData == null)
            {
                return BadRequest("No Product Find");
            }
            return Ok(productData);
        }


        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<Product>> AddProduct(ProductModel product)
        {
            try
            {
                if (product is null)
                {
                    throw new ArgumentNullException(nameof(product));
                }
                var ProductDetail = new Product()
                {
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    IsActive = product.IsActive,
                    ProductCategoryId = product.ProductCategoryId,
                    ProductGroupId = product.ProductGroupId,
                    ProductTypeId = product.ProductTypeId,
                    CreatedBy = product.CreatedBy,
                    CreatedDateTime = product.CreatedDateTime,
                };
                _shivaEnterpriseContext.products.Add(ProductDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteProduct(Guid productId)
        {
            var deleteProduct = _shivaEnterpriseContext.accounts.Find(productId);
            if (deleteProduct != null)
            {
                _shivaEnterpriseContext.Entry(deleteProduct).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditProduct")]
        public async Task<IActionResult> EditProductDetail(Guid id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.products.Any(x => x.ProductId == id))
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

