using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Accounts;
using Shiva_Enterprise_APIs.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class IssueController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public IssueController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetIssue")]
        public async Task<ActionResult> GetAllIssue()
        {
            var issue = await _shivaEnterpriseContext.Issues.ToListAsync();

            if (issue == null)
                return NotFound();
            return Ok(issue);
        }

        [HttpGet]
        [Route("GetIssueById")]
        public async Task<ActionResult> GetIssueById(Guid issueId)
        {
            if (issueId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(issueId));
            }

            var issueData = await _shivaEnterpriseContext.Issues.FindAsync(issueId);
            if (issueData == null)
            {
                return BadRequest("No Issue Find");
            }
            return Ok(issueData);
        }


        [HttpPost]
        [Route("AddIssue")]
        public async Task<ActionResult<Issue>> AddIssue(Issue issue)
        {
            try
            {
                if (issue is null)
                {
                    throw new ArgumentNullException(nameof(issue));
                }
                var IssueDetail = new Issue()
                {
                    IssueCode = issue.IssueCode,
                    IssueName = issue.IssueName,
                    IssueDescription = issue.IssueDescription,
                    IsActive = issue.IsActive,
                    CreatedBy = issue.CreatedBy,
                    CreatedDateTime = issue.CreatedDateTime,
                };
                _shivaEnterpriseContext.Issues.Add(IssueDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteIssue")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteIssue(Guid issueId)
        {
            var deleteIssue = _shivaEnterpriseContext.Issues.Find(issueId);
            if (deleteIssue != null)
            {
                _shivaEnterpriseContext.Entry(deleteIssue).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditIssue")]
        public async Task<IActionResult> EditIssueDetail(Guid id, Issue issue)
        {
            if (id != issue.IssueId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(issue).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Issues.Any(x => x.IssueId == id))
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
