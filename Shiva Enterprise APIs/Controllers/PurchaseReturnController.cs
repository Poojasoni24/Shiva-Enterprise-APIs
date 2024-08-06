using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Purchase;
using Shiva_Enterprise_APIs.Model.Purchase;

namespace Shiva_Enterprise_APIs.Controllers
{
    public class PurchaseReturnController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;

        public PurchaseReturnController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        // GET: api/PurchaseReturn
        [HttpGet]
        public async Task<ActionResult> GetPurchaseReturns()
        {
            var purchaseReturns = await _shivaEnterpriseContext.PurchaseReturns.ToListAsync();
            return Ok(purchaseReturns);
        }

        // GET: api/PurchaseReturn/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPurchaseReturn(int id)
        {
            var purchaseReturn = await _shivaEnterpriseContext.PurchaseReturns.FindAsync(id);

            if (purchaseReturn == null)
            {
                return NotFound();
            }

            return Ok(purchaseReturn);
        }

        // POST: api/PurchaseReturn
        [HttpPost]
        public async Task<ActionResult> AddPurchaseReturn(PurchaseReturn purchaseReturn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _shivaEnterpriseContext.PurchaseReturns.Add(purchaseReturn);
            await _shivaEnterpriseContext.SaveChangesAsync();

            return Ok(purchaseReturn.PurchaseReturnId);
        }

        // PUT: api/PurchaseReturn/5
        [HttpPut("{id}")]
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

        // DELETE: api/PurchaseReturn/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchaseReturn(int id)
        {
            var purchaseReturn = await _shivaEnterpriseContext.PurchaseReturns.FindAsync(id);
            if (purchaseReturn == null)
            {
                return NotFound();
            }

            _shivaEnterpriseContext.PurchaseReturns.Remove(purchaseReturn);
            await _shivaEnterpriseContext.SaveChangesAsync();

            return Ok();
        }

    }
}
