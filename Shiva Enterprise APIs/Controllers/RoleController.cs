using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Model;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class RoleController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(ShivaEnterpriseContext shivaEnterpriseContext, RoleManager<ApplicationRole> roleManager)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
            this.roleManager = roleManager;
        }

        [Route("GetRoles")]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _shivaEnterpriseContext.applicationRoles.ToListAsync();
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }

        [Route("GetRoleById")]
        [HttpGet]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound(); // Role not found
            }

            return Ok(role);
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel role)
        {
            //if (!await _roleManager.RoleExistsAsync(role.Name))
            //{
            var applicationRole = new ApplicationRole()
            {
                Name = role.Name,
                NormalizedName = role.NormalizedName,
                IsActive = role.IsActive,
                CreatedBy = role.CreatedBy,
                CreatedDateTime = DateTime.Now  
            };

            var result = await roleManager.CreateAsync(applicationRole);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Role created successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Failed to create role" });
            }

            //}

        }

        [Route("UpdateRole")]
        [HttpPut]
        public async Task<IActionResult> UpdateRole(string roleId, RoleModel roleObj)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound(); // Role not found
            }
            else
            {
                role.Name = roleObj.Name;
                role.IsActive = roleObj.IsActive;
            }

            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Role updated successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Failed to update role", Errors = result.Errors });
            }
        }

        [Route("DeleteRole")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound(); // Role not found
            }

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role deleted successfully");
            }
            else
            {
                return BadRequest(new { Message = "Failed to delete role", Errors = result.Errors });
            }
        }
    }
}
