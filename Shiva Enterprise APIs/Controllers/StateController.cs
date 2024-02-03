using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Model;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class StateController : Controller
    {
        private readonly ShivaEnterpriseContext _shivaEnterpriseContext;

        public StateController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
                _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetAllState")]
        public async Task<IActionResult> GetAllState()
        {
            var stateDetail = await _shivaEnterpriseContext.states.ToListAsync();
            if (stateDetail == null)
            {
                return NotFound();
            }
            return Ok(stateDetail);
        }

        [HttpGet]
        [Route("GetStateById")]
        public async Task<IActionResult> GetStateById(Guid stateId)
        {
            if (stateId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(stateId));
            }

            var stateData = await _shivaEnterpriseContext.states.FindAsync(stateId);
            if (stateData == null)
            {
                return BadRequest("No state Find");
            }
            return Ok(stateData);
        }


        [HttpPost]
        [Route("AddState")]
        public async Task<ActionResult<State>> AddState(StateModel stateObj)
        {
            try
            {
                if (stateObj is null)
                {
                    throw new ArgumentNullException(nameof(stateObj));
                }
                var stateDetail = new State()
                {
                    State_Code = stateObj.State_Code,
                    State_Name = stateObj.State_Name,
                    Country_Id = stateObj.Country_Id,
                    CreatedBy = stateObj.CreatedBy,
                    IsActive = stateObj.IsActive,
                    CreatedDateTime = stateObj.CreatedDateTime,
                };
                _shivaEnterpriseContext.states.Add(stateDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteState")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteState(Guid stateId)
        {
            var stateDetail = _shivaEnterpriseContext.states.Find(stateId);
            if (stateDetail != null)
            {
                _shivaEnterpriseContext.Entry(stateDetail).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditState")]
        public async Task<IActionResult> EditStateDetail(Guid id, State state)
        {
            if (id != state.State_Id)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(state).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.states.Any(x => x.State_Id == id))
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
