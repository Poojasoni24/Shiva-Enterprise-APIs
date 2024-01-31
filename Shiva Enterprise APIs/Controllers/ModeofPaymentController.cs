﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ModeofPaymentController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public ModeofPaymentController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetModeofPayment")]
        public async Task<ActionResult> GetAllModeofPayment()
        {
            var mod = await _shivaEnterpriseContext.ModeofPayments.ToListAsync();

            if (mod == null)
                return NotFound();
            return Ok(mod);
        }

        [HttpGet]
        [Route("GetModById")]
        public async Task<ActionResult> GetModById(Guid modId)
        {
            if (modId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(modId));
            }

            var modData = await _shivaEnterpriseContext.ModeofPayments.FindAsync(modId);
            if (modData == null)
            {
                return BadRequest("No ModeofPayments data Find");
            }
            return Ok(modData);
        }


        [HttpPost]
        [Route("AddModeofPayment")]
        public async Task<ActionResult<ModeofPayment>> AddModeofPayment(ModeofPayment addModeofPaymentObj)
        {
            try
            {
                if (addModeofPaymentObj is null)
                {
                    throw new ArgumentNullException(nameof(addModeofPaymentObj));
                }
                _shivaEnterpriseContext.ModeofPayments.Add(addModeofPaymentObj);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteModeofPayment")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteModeofPayment(Guid modId)
        {
            var deleteModeofPayment = _shivaEnterpriseContext.ModeofPayments.Find(modId);
            if (deleteModeofPayment != null)
            {
                _shivaEnterpriseContext.Entry(deleteModeofPayment).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditModeofPayment")]
        public async Task<IActionResult> EditModeofPaymentDetail(Guid id, ModeofPayment modeofpayment)
        {
            if (id != modeofpayment.MODId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(modeofpayment).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.ModeofPayments.Any(x => x.MODId == id))
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


