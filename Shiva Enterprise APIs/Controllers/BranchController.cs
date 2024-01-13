using Microsoft.AspNetCore.Authorization;
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
    public class BranchController : Controller
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;

        public BranchController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }
        [HttpGet]
        [Route("GetAllBranch")]
        public async Task<ActionResult> GetAllBranch()
        {
            var branches = await _shivaEnterpriseContext.Branches.ToListAsync();

            if (branches == null)
                return NotFound();
            return Ok(branches);
        }

        [HttpGet]
        [Route("GetBranchById")]
        public async Task<ActionResult> GetBranchById(Guid branchId)
        {
            if (branchId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(branchId));
            }

            var branchdata = await _shivaEnterpriseContext.Branches.FindAsync(branchId);
            if (branchdata == null)
            {
                return BadRequest("No branch Find");
            }
            return Ok(branchdata);
        }


        [HttpPost]
        [Route("AddBranch")]
        public async Task<ActionResult<BranchModel>> AddBranch(BranchModel branch)
        {
            try
            {
                var branchdetail = new BranchModel()
                {
                    Branch_Code = branch.Branch_Code,
                    Branch_Name = branch.Branch_Name,
                    Company_Id = branch.Company_Id,
                    IsActive = branch.IsActive,
                };
                
                _shivaEnterpriseContext.Branches.Add(ModelToEntity(branchdetail));
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteBranch")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteBranch(Guid branchId)
        {
            var branchdata = _shivaEnterpriseContext.Branches.Find(branchId);
            if (branchdata != null)
            {
                _shivaEnterpriseContext.Entry(branchdata).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditBranch")]
        public async Task<IActionResult> EditBranchDetail(Guid id, Branch branch)
        {
            if (id != branch.Branch_Id)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(branch).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Branches.Any(x => x.Branch_Id == id))
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
        private Branch ModelToEntity(BranchModel branchModel)
        {
            return new Branch()
            {
                Branch_Code = branchModel.Branch_Code,
                Branch_Name = branchModel.Branch_Name,
                Company_Id = branchModel.Company_Id,
                IsActive = branchModel.IsActive,
                CreatedDateTime = branchModel.CreatedDateTime,
                UpdatedDateTime = branchModel.UpdatedDateTime,
            };
        }
        #endregion

    }
}
