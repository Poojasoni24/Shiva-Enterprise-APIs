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
    public class AccountTypeController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public AccountTypeController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetAccountType")]
        public async Task<ActionResult> GetAllAccountType()
        {
            var accountType = await _shivaEnterpriseContext.accountTypes.ToListAsync();

            if (accountType == null)
                return NotFound();
            return Ok(accountType);
        }

        [HttpGet]
        [Route("GetAccountTypeById")]
        public async Task<ActionResult> GetAccountTypeById(Guid accountTypeId)
        {
            if (accountTypeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(accountTypeId));
            }

            var accountTypeData = await _shivaEnterpriseContext.accountTypes.FindAsync(accountTypeId);
            if (accountTypeData == null)
            {
                return BadRequest("No AccountTypes Find");
            }
            return Ok(accountTypeData);
        }


        [HttpPost]
        [Route("AddAccountType")]
        public async Task<ActionResult<AccountType>> AddAccountType(AccountType addAccountTypeObj)
        {
            try
            {
                if (addAccountTypeObj is null)
                {
                    throw new ArgumentNullException(nameof(addAccountTypeObj));
                }
                _shivaEnterpriseContext.accountTypes.Add(addAccountTypeObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteAccountType")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteAccountType(Guid accountTypeId)
        {
            var deleteAccountType = _shivaEnterpriseContext.accountTypes.Find(accountTypeId);
            if (deleteAccountType != null)
            {
                _shivaEnterpriseContext.Entry(deleteAccountType).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditAccountType")]
        public async Task<IActionResult> EditAccountTypeDetail(Guid id, AccountType accountType)
        {
            if (id != accountType.AccountTypeId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(accountType).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.accountTypes.Any(x => x.AccountTypeId == id))
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
