using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Model.Product;


namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductGroupController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public ProductGroupController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetProductGroup")]
        public async Task<ActionResult> GetAllProductGroup()
        {
            var productGroup = await _shivaEnterpriseContext.productGroups.ToListAsync();

            if (productGroup == null)
                return NotFound();
            return Ok(productGroup);
        }

        [HttpGet]
        [Route("GetProductGroupById")]
        public async Task<ActionResult> GetProductGroupById(Guid productGroupId)
        {
            if (productGroupId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productGroupId));
            }

            var productGroupData = await _shivaEnterpriseContext.accountGroups.FindAsync(productGroupId);
            if (productGroupData == null)
            {
                return BadRequest("No ProductGroups Find");
            }
            return Ok(productGroupData);
        }


        [HttpPost]
        [Route("AddProductGroup")]
        public async Task<ActionResult<ProductGroup>> AddProductGroup(ProductGroupModel productGroup)
        {
            try
            {
                if (productGroup is null)
                {
                    throw new ArgumentNullException(nameof(productGroup));
                }
                var ProductGroupDetail = new ProductGroup()
                {
                    ProductGroupCode = productGroup.ProductGroupCode,
                    ProductGroupName = productGroup.ProductGroupName,
                    ProductGroupDescription = productGroup.ProductGroupDescription,
                    IsActive = productGroup.IsActive,
                    CreatedBy = productGroup.CreatedBy,
                    CreatedDateTime = productGroup.CreatedDateTime,
                };
                _shivaEnterpriseContext.productGroups.Add(ProductGroupDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");    
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteProductGroup")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteProductGroup(Guid productGroupId)
        {
            var deleteProductGroup = _shivaEnterpriseContext.productGroups.Find(productGroupId);
            if (deleteProductGroup != null)
            {
                _shivaEnterpriseContext.Entry(deleteProductGroup).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditProductGroup")]
        public async Task<IActionResult> EditProductGroupDetail(Guid id, ProductGroup productGroup)
        {
            if (id != productGroup.ProductGroupId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(productGroup).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.productGroups.Any(x => x.ProductGroupId == id))
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
