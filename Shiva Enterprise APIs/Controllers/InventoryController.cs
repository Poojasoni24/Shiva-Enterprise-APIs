using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Model;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class InventoryController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public InventoryController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetAllInventory")]
        public async Task<ActionResult> GetAllInventory()
        {
            var inventory = await _shivaEnterpriseContext.Inventory.ToListAsync();

            if (inventory == null)
                return NotFound();
            return Ok(inventory);
        }

        [HttpPost]
        [Route("AddEditInventoryDetails")]
        public async Task<ActionResult<InventoryModel>> AddEditInventoryDetails(List<InventoryModel> inventory)
        {
            try
            {
                if (inventory is null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No inventory to add.");
                }

                foreach (InventoryModel inventoryModelDetail in inventory)
                {
                    if (_shivaEnterpriseContext.Inventory.Where<Inventory>(s => s.ProductId == inventoryModelDetail.ProductId).AnyAsync().Result)
                    {
                        Inventory Detail = await _shivaEnterpriseContext.Inventory.Where<Inventory>(s => s.ProductId == inventoryModelDetail.ProductId).FirstAsync();
                        Detail.OpeningQty += inventoryModelDetail.OpeningQty;
                        Detail.ClosingQty += inventoryModelDetail.ClosingQty;
                        Detail.InQuantity += inventoryModelDetail.InQuantity;
                        Detail.OutQuantity += inventoryModelDetail.OutQuantity;
                        Detail.InventoryCost = inventoryModelDetail.InventoryCost;
                        Detail.TransactionDate = inventoryModelDetail.TransactionDate;
                        Detail.ModifiedBy = inventoryModelDetail.ModifiedBy;

                        _shivaEnterpriseContext.Entry(Detail).State = EntityState.Modified;

                        await _shivaEnterpriseContext.SaveChangesAsync();
                    }
                    else
                    {
                        var Detail = new Inventory()
                        {
                            InventoryCode = inventoryModelDetail.InventoryCode,
                            ProductId = inventoryModelDetail.ProductId,
                            OpeningQty = inventoryModelDetail.OpeningQty,
                            ClosingQty = inventoryModelDetail.ClosingQty,
                            InQuantity = inventoryModelDetail.InQuantity,
                            OutQuantity = inventoryModelDetail.OutQuantity,
                            InventoryCost = inventoryModelDetail.InventoryCost,
                            TransactionDate = inventoryModelDetail.TransactionDate,
                            ModifiedBy = inventoryModelDetail.ModifiedBy
                        };

                        _shivaEnterpriseContext.Inventory.Add(Detail);
                        await _shivaEnterpriseContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }

            return Ok("Added Successfully");
        }
    }
}
