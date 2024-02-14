using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;

using Shiva_Enterprise_APIs.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class UnitController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public UnitController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetUnit")]
        public async Task<ActionResult> GetAllUnit()
        {
            var unit = await _shivaEnterpriseContext.Units.ToListAsync();

            if (unit == null)
                return NotFound();
            return Ok(unit);
        }

        [HttpGet]
        [Route("GetUnitById")]
        public async Task<ActionResult> GetUnitById(Guid unitId)
        {
            if (unitId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(unitId));
            }

            var unitData = await _shivaEnterpriseContext.Units.FindAsync(unitId);
            if (unitData == null)
            {
                return BadRequest("No Unit Find");
            }
            return Ok(unitData);
        }


        [HttpPost]
        [Route("AddUnit")]
        public async Task<ActionResult<Unit>> AddUnit(UnitModel unit)
        {
            try
            {
                if (unit is null)
                {
                    throw new ArgumentNullException(nameof(unit));
                }
                var UnitDetail = new Unit()
                {
                    UnitCode = unit.UnitCode,
                    UnitName = unit.UnitName,
                    UnitDescription = unit.UnitDescription,
                    IsActive = unit.IsActive,
                    CreatedBy = unit.CreatedBy,
                    CreatedDateTime = unit.CreatedDateTime,
                };
                _shivaEnterpriseContext.Units.Add(UnitDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteUnit")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteUnit(Guid unitId)
        {
            var deleteUnit = _shivaEnterpriseContext.Units.Find(unitId);
            if (deleteUnit != null)
            {
                _shivaEnterpriseContext.Entry(deleteUnit).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditUnit")]
        public async Task<IActionResult> EditUnitDetail(Guid id, Unit unit)
        {
            if (id != unit.UnitId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(unit).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Units.Any(x => x.UnitId == id))
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
