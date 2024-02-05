using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Accounts;
using Shiva_Enterprise_APIs.Model.Account;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public AccountController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetAccount")]
        public async Task<ActionResult> GetAllAccount()
        {
            var account = await _shivaEnterpriseContext.accounts.ToListAsync();

            if (account == null)
                return NotFound();
            return Ok(account);
        }

        [HttpGet]
        [Route("GetAccountById")]
        public async Task<ActionResult> GetAccountById(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var accountData = await _shivaEnterpriseContext.accounts.FindAsync(accountId);
            if (accountData == null)
            {
                return BadRequest("No Accounts Find");
            }
            return Ok(accountData);
        }


        [HttpPost]
        [Route("AddAccount")]
        public async Task<ActionResult<Account>> AddAccount(AccountModel account)
        {
            try
            {
                if (account is null)
                {
                    throw new ArgumentNullException(nameof(account));
                }
                var AccountDetail = new Account()
                {
                    AccountCode = account.AccountCode,
                    AccontName = account.AccountName,
                    AccountDescription = account.AccountDescription,
                    IsActive = account.IsActive,
                    AccountGroupId = account.AccountGroupId,
                    AccountTypeId = account.AccountTypeId,
                    AccountCategoryId = account.AccountCategoryId,
                    CreatedBy = account.CreatedBy,
                    CreatedDateTime = account.CreatedDateTime,
                };
                _shivaEnterpriseContext.accounts.Add(AccountDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteAccount")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteAccount(Guid accountId)
        {
            var deleteAccount = _shivaEnterpriseContext.accounts.Find(accountId);
            if (deleteAccount != null)
            {
                _shivaEnterpriseContext.Entry(deleteAccount).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditAccount")]
        public async Task<IActionResult> EditAccountDetail(Guid id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(account).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.accounts.Any(x => x.AccountId == id))
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
