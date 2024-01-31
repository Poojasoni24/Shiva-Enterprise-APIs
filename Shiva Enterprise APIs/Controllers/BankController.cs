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
    public class BankController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public BankController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetBank")]
        public async Task<ActionResult> GetAllBank()
        {
            var bank = await _shivaEnterpriseContext.Banks.ToListAsync();

            if (bank == null)
                return NotFound();
            return Ok(bank);
        }

        [HttpGet]
        [Route("GetBankById")]
        public async Task<ActionResult> GetBankById(Guid bankId)
        {
            if (bankId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bankId));
            }

            var bankData = await _shivaEnterpriseContext.Banks.FindAsync(bankId);
            if (bankData == null)
            {
                return BadRequest("No Bank Find");
            }
            return Ok(bankData);
        }


        [HttpPost]
        [Route("AddBank")]
        public async Task<ActionResult<Bank>> AddBank(Bank addBankObj)
        {
            try
            {
                if (addBankObj is null)
                {
                    throw new ArgumentNullException(nameof(addBankObj));
                }
                _shivaEnterpriseContext.Banks.Add(addBankObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteBank")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteBank(Guid bankId)
        {
            var deleteBank = _shivaEnterpriseContext.Banks.Find(bankId);
            if (deleteBank != null)
            {
                _shivaEnterpriseContext.Entry(deleteBank).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditBank")]
        public async Task<IActionResult> EditBankDetail(Guid id, Bank bank)
        {
            if (id != bank.BankId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(bank).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Banks.Any(x => x.BankId == id))
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
