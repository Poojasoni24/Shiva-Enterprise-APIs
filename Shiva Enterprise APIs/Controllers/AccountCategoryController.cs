using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Accounts;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountCategoryController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public AccountCategoryController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }
        [HttpGet]
        [Route("GetAccountCategory")]
        public async Task<ActionResult> GetAllAccountCategory()
        {
            var accountCategory = await _shivaEnterpriseContext.accountCategories.ToListAsync();

            if (accountCategory == null)
                return NotFound();
            return Ok(accountCategory);
        }

        [HttpGet]
        [Route("GetAccountCategoryById")]
        public async Task<ActionResult> GetAccountCategoryById(Guid accountCategoryId)
        {
            if (accountCategoryId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(accountCategoryId));
            }

            var accountCategoryData = await _shivaEnterpriseContext.accountCategories.FindAsync(accountCategoryId);
            if (accountCategoryData == null)
            {
                return BadRequest("No AccountCategorys Find");
            }
            return Ok(accountCategoryData);
        }


        [HttpPost]
        [Route("AddAccountCategory")]
        public async Task<ActionResult<AccountCategory>> AddAccountCategory(AccountCategory addAccountCategoryObj)
        {
            try
            {
                if (addAccountCategoryObj is null)
                {
                    throw new ArgumentNullException(nameof(addAccountCategoryObj));
                }
                _shivaEnterpriseContext.accountCategories.Add(addAccountCategoryObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteAccountCategory")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteAccountCategory(Guid accountCategoryId)
        {
            var deleteAccountCategory = _shivaEnterpriseContext.accountCategories.Find(accountCategoryId);
            if (deleteAccountCategory != null)
            {
                _shivaEnterpriseContext.Entry(deleteAccountCategory).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditAccountCategory")]
        public async Task<IActionResult> EditAccountCategoryDetail(Guid id, AccountCategory accountCategory)
        {
            if (id != accountCategory.AccountCategoryId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(accountCategory).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.accountCategories.Any(x => x.AccountCategoryId == id))
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
