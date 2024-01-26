using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Authentication;
using Shiva_Enterprise_APIs.Model;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class SalesmanAgent : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;

        public SalesmanAgent(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }
        [HttpGet]
        [Route("GetAllSalesmanAgent")]
        public async Task<ActionResult> GetAllSalesmanAgent()
        {
            var salesmanAgents = await _shivaEnterpriseContext.salesmanAgents.ToListAsync();

            if (salesmanAgents == null)
                return NotFound();
            return Ok(salesmanAgents);
        }

        [HttpGet]
        [Route("GetSalesmanAgentById")]
        public async Task<ActionResult> GetSalesmanAgentById(Guid salesmanAgentId)
        {
            if (salesmanAgentId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(salesmanAgentId));
            }

            var salesmanAgentdata = await _shivaEnterpriseContext.salesmanAgents.FindAsync(salesmanAgentId);
            if (salesmanAgentdata == null)
            {
                return BadRequest("No SalesmanAgent Find");
            }
            return Ok(salesmanAgentdata);
        }


        [HttpPost]
        [Route("AddsalesmanAgent")]
        public async Task<ActionResult<SalesmanAgentModel>> AddsalesmanAgent(SalesmanAgentModel salesmanAgent)
        {
            try
            {
                var SalesmanAgentdetail = new SalesmanAgentModel()
                {
                    Salesman_Name = salesmanAgent.Salesman_Name,
                    Salesman_email = salesmanAgent.Salesman_email,
                    Salesman_code = salesmanAgent.Salesman_code,
                    Status = salesmanAgent.Status,
                    Salesmanphone = salesmanAgent.Salesmanphone,
                    CreatedBy = salesmanAgent.CreatedBy,
                    ModifiedBy = salesmanAgent.ModifiedBy,

                    
                };

                _shivaEnterpriseContext.salesmanAgents.Add(ModelToEntity(SalesmanAgentdetail));
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteSalesmanAgent")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteSalesmanAgent(Guid salesmanAgentId)
        {
            var Salesmanagentdata = _shivaEnterpriseContext.salesmanAgents.Find(salesmanAgentId);
            if (Salesmanagentdata != null)
            {
                _shivaEnterpriseContext.Entry(Salesmanagentdata).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditsalesmanAgent")]
        public async Task<IActionResult> EditsalesmanAgentDetail(Guid id, salesmanAgent salesmanAgent)
        {
            if (id != salesmanAgent.SalesmanAgentID)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(salesmanAgent).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.salesmanAgents.Any(x => x.SalesmanAgentID == id))
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

        #region Private Method
        private salesmanAgent ModelToEntity(SalesmanAgentModel salesmanAgentModel)      
        {
            return new salesmanAgent()
            {
                Salesman_Name = salesmanAgentModel.Salesman_Name,
                Salesman_email = salesmanAgentModel.Salesman_email,
                Salesman_code = salesmanAgentModel.Salesman_code,
                Status = salesmanAgentModel.Status,
                Salesmanphone = salesmanAgentModel.Salesmanphone,
                CreatedBy = salesmanAgentModel.CreatedBy,
                ModifiedBy = salesmanAgentModel.ModifiedBy,
                CreatedDateTime = salesmanAgentModel.CreatedDateTime,
                ModifiedDateTime = salesmanAgentModel.ModifiedDateTime,

                
            };
        }
        #endregion

    }
    
    }

