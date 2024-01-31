using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Accounts;
using Shiva_Enterprise_APIs.Entities.TransportEntities;
using Shiva_Enterprise_APIs.Model.Transport;


namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class TransportController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public TransportController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetTransport")]
        public async Task<ActionResult> GetAllTransport()
        {
            var transport = await _shivaEnterpriseContext.Transports.ToListAsync();

            if (transport == null)
                return NotFound();
            return Ok(transport);
        }

        [HttpGet]
        [Route("GetTransportById")]
        public async Task<ActionResult> GetTransportById(Guid transportId)
        {
            if (transportId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(transportId));
            }

            var transportData = await _shivaEnterpriseContext.Transports.FindAsync(transportId);
            if (transportData == null)
            {
                return BadRequest("No Transports Find");
            }
            return Ok(transportData);
        }


        [HttpPost]
        [Route("AddTransport")]
        public async Task<ActionResult<TransportModel>> AddTransport(Transport addTransportObj)
        {
            try
            {
                if (addTransportObj is null)
                {
                    throw new ArgumentNullException(nameof(addTransportObj));
                }
                _shivaEnterpriseContext.Transports.Add(addTransportObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteTransport")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteTransport(Guid transportId)
        {
            var deleteTransport = _shivaEnterpriseContext.Transports.Find(transportId);
            if (deleteTransport != null)
            {
                _shivaEnterpriseContext.Entry(deleteTransport).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditTransport")]
        public async Task<IActionResult> EditTransportDetail(Guid id, Transport transport)
        {
            if (id != transport.TransportId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(transport).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Transports.Any(x => x.TransportId == id))
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
