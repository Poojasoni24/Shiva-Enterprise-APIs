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
    public class AccountGroupController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public AccountGroupController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetAccountGroup")]
        public async Task<ActionResult> GetAllAccountGroup()
        {
            var accountGroup = await _shivaEnterpriseContext.accountGroups.ToListAsync();

            if (accountGroup == null)
                return NotFound();
            return Ok(accountGroup);
        }

        [HttpGet]
        [Route("GetAccountGroupById")]
        public async Task<ActionResult> GetAccountGroupById(Guid accountGroupId)
        {
            if (accountGroupId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(accountGroupId));
            }

            var accountGroupData = await _shivaEnterpriseContext.accountGroups.FindAsync(accountGroupId);
            if (accountGroupData == null)
            {
                return BadRequest("No AccountGroups Find");
            }
            return Ok(accountGroupData);
        }


        [HttpPost]
        [Route("AddAccountGroup")]
        public async Task<ActionResult<AccountGroup>> AddAccountGroup(AccountGroup addAccountGroupObj)
        {
            try
            {
                if (addAccountGroupObj is null)
                {
                    throw new ArgumentNullException(nameof(addAccountGroupObj));
                }
                _shivaEnterpriseContext.accountGroups.Add(addAccountGroupObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteAccountGroup")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteAccountGroup(Guid accountGroupId)
        {
            var deleteAccountGroup = _shivaEnterpriseContext.accountGroups.Find(accountGroupId);
            if (deleteAccountGroup != null)
            {
                _shivaEnterpriseContext.Entry(deleteAccountGroup).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditAccountGroup")]
        public async Task<IActionResult> EditAccountGroupDetail(Guid id, AccountGroup accountGroup)
        {
            if (id != accountGroup.AccountGroupId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(accountGroup).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.accountGroups.Any(x => x.AccountGroupId == id))
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
