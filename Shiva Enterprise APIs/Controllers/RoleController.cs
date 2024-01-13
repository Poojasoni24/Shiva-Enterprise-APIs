using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shiva_Enterprise_APIs.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET api/role
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);
        }

        // GET api/role/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound(); // Role not found
            }

            return Ok(role);
        }

        // POST api/role
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var newRole = new IdentityRole(roleName);

            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Role created successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Failed to create role", Errors = result.Errors });
            }
        }

        // PUT api/role/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound(); // Role not found
            }

            role.Name = newRoleName;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Role updated successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Failed to update role", Errors = result.Errors });
            }
        }

        // DELETE api/role/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound(); // Role not found
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Role deleted successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Failed to delete role", Errors = result.Errors });
            }
        }
    }
}
