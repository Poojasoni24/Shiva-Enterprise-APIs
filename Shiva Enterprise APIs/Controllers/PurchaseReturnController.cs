using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model.Purchase;

namespace Shiva_Enterprise_APIs.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class PurchaseReturnController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;

        public PurchaseReturnController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetPurchaseReturns")]
        public async Task<ActionResult> GetPurchaseReturns()
        {
            var purchaseReturns = await _shivaEnterpriseContext.PurchaseReturns.ToListAsync();
            if (purchaseReturns == null)
                return NotFound();
            return Ok(purchaseReturns);
        }

        [HttpGet]
        [Route("GetPurchaseReturnById")]
        public async Task<ActionResult> GetPurchaseReturnById(int PurchasereturnId)
        {
            if (PurchasereturnId == null)
            {
                throw new ArgumentNullException(nameof(PurchasereturnId));
            }

            var purchaseReturn = await _shivaEnterpriseContext.PurchaseReturns.FindAsync(PurchasereturnId);
            if (purchaseReturn == null)
            {
                return BadRequest("No Purchase Return Find");
            }
            return Ok(purchaseReturn);
        }

        [HttpPost]
        [Route("AddPurchaseReturn")]
        public async Task<ActionResult<PurchaseReturn>> AddPurchaseReturn(PurchaseReturn purchaseReturn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _shivaEnterpriseContext.PurchaseReturns.Add(purchaseReturn);
            await _shivaEnterpriseContext.SaveChangesAsync();

            return Ok(purchaseReturn.PurchaseReturnId);
        }

        [HttpPost]
        [Route("DeletePurchaseReturn")]
        public async Task<ActionResult> DeletePurchaseReturn(int id)
        {
            var purchaseReturn = await _shivaEnterpriseContext.PurchaseReturns.FindAsync(id);
            if (purchaseReturn == null)
            {
                return NotFound();
            }

            _shivaEnterpriseContext.Entry(purchaseReturn).State = EntityState.Deleted;
            await _shivaEnterpriseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("EditPurchaseReturn")]
        public async Task<IActionResult> EditPurchaseReturn(int id, PurchaseReturn purchaseReturn)
        {
            if (id != purchaseReturn.PurchaseReturnId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(purchaseReturn).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.PurchaseReturns.Any(e => e.PurchaseReturnId == id))
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
