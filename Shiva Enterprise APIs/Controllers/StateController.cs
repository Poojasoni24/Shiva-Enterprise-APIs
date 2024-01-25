using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;

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
        public async Task<IActionResult> GetStateById(string stateId)
        {
            if (string.IsNullOrEmpty(stateId))
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
        public async Task<ActionResult<state>> AddState(state stateObj)
        {
            try
            {
                if (stateObj is null)
                {
                    throw new ArgumentNullException(nameof(stateObj));
                }
                _shivaEnterpriseContext.states.Add(stateObj);
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
        public async Task<ActionResult<ApiResponseFormat>> DeleteState(string stateId)
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
        public async Task<IActionResult> EditStateDetail(Guid id, state state)
        {
            if (id != state.State_ID)
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
                if (!_shivaEnterpriseContext.states.Any(x => x.State_ID == id))
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
