using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Products;
using Shiva_Enterprise_APIs.Model;
using Shiva_Enterprise_APIs.Model.Product;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class StockController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public StockController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetAllStock")]
        public async Task<ActionResult> GetAllStock()
        {
            var stock = await _shivaEnterpriseContext.Stock.ToListAsync();

            if (stock == null)
                return NotFound();
            return Ok(stock);
        }

        [HttpGet]
        [Route("GetStockByProductId")]
        public async Task<ActionResult> GetStockByProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            var stock = await _shivaEnterpriseContext.Stock.Where<Stock>(s => s.ProductId == productId).FirstAsync();
            if (stock == null)
            {
                return BadRequest("No Stock Found");
            }

            return Ok(stock);
        }

        [HttpPost]
        [Route("AddEditStockDetails")]
        public async Task<ActionResult<Stock>> AddEditStockDetails(List<StockModel> stock)
        {
            try
            {
                if (stock is null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "No stock to add.");
                }

                foreach (StockModel stockModelDetail in stock)
                {
                    if (_shivaEnterpriseContext.Stock.Where<Stock>(s => s.ProductId == stockModelDetail.ProductId).AnyAsync().Result)
                    {
                        Stock StockDetail = await _shivaEnterpriseContext.Stock.Where<Stock>(s => s.ProductId == stockModelDetail.ProductId).FirstAsync();
                        StockDetail.QuantityOnHand += stockModelDetail.QuantityOnHand;
                        StockDetail.ReorderLevel = StockDetail.QuantityOnHand <= 20 ? "Low Stock" : "Stock Available";
                        StockDetail.ModifiedBy = stockModelDetail.ModifiedBy;

                        _shivaEnterpriseContext.Entry(StockDetail).State = EntityState.Modified;

                        await _shivaEnterpriseContext.SaveChangesAsync();
                    }
                    else
                    {
                        var StockDetail = new Stock()
                        {
                            StockCode = stockModelDetail.StockCode,
                            ProductId = stockModelDetail.ProductId,
                            QuantityOnHand = stockModelDetail.QuantityOnHand,
                            ReorderLevel = stockModelDetail.QuantityOnHand <= 20 ? "Low Stock" : "Stock Available",
                            ModifiedBy = stockModelDetail.ModifiedBy
                        };

                        _shivaEnterpriseContext.Stock.Add(StockDetail);
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


        [HttpPost]
        [Route("AddStock")]
        public async Task<ActionResult<Stock>> AddStock(StockModel stock)
        {
            try
            {
                if (stock is null)
                {
                    throw new ArgumentNullException(nameof(stock));
                }

                var StockDetail = new Stock()
                {
                    StockCode = stock.StockCode,
                    ProductId = stock.ProductId,
                    QuantityOnHand = stock.QuantityOnHand,
                    ReorderLevel = stock.ReorderLevel,
                    ModifiedBy = stock.ModifiedBy
                };

                _shivaEnterpriseContext.Stock.Add(StockDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();

                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteStock")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteStock(Guid productId)
        {
            var deleteStock = _shivaEnterpriseContext.Stock.Where<Stock>(s => s.ProductId == productId).First<Stock>();
            if (deleteStock != null)
            {
                _shivaEnterpriseContext.Entry(deleteStock).State = EntityState.Deleted;
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }

            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditStock")]
        public async Task<IActionResult> EditStock(Guid productId, StockModel stock)
        {
            if (productId != stock.ProductId)
            {
                return BadRequest();
            }

            Stock stockDetails = new Stock
            {
                StockId = stock.StockId,
                StockCode = stock.StockCode,
                ProductId = stock.ProductId,
                ReorderLevel = stock.ReorderLevel,
                QuantityOnHand = stock.QuantityOnHand,
                ModifiedBy = stock.ModifiedBy
            };

            _shivaEnterpriseContext.Entry(stockDetails).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Stock.Where<Stock>(x => x.ProductId == productId).AnyAsync().Result)
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
