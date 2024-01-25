using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Entities.Authentication;
using Shiva_Enterprise_APIs.Model;
using System.Security;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public UserController(ShivaEnterpriseContext shivaEnterpriseContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async Task<ActionResult> GetAllUsers()
        {
            var user = await _shivaEnterpriseContext.applicationUsers.ToListAsync();

            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<ActionResult> GetUserById(string userId)
        {
            var userdata = await userManager.FindByIdAsync(userId);
            if (userdata == null)
            {
                return BadRequest("No user Find");
            }
            return Ok(userdata);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteUser(Guid userId)
        {
            var userdata = await userManager.FindByIdAsync(userId.ToString());
            if (userdata != null)
            {
                _shivaEnterpriseContext.Entry(userdata).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditUser")]
        public async Task<IActionResult> EditUserDetail(string id, ApplicationUser model)
        {
            // Find the user by ID
            var userdetail = await userManager.FindByIdAsync(id);

            if (userdetail == null)
            {
                return NotFound(); // Or handle the case where the user is not found
            }

            // Update user details
            userdetail.UserName = model.UserName;
            userdetail.Email = model.Email;
            userdetail.First_Name = model.First_Name;
            userdetail.Middle_Name = model.Middle_Name;
            userdetail.Last_Name = model.Last_Name;
            userdetail.PhoneNumber = model.PhoneNumber;
            userdetail.IsActive = model.IsActive;
            userdetail.CreatedDateAndTime = model.CreatedDateAndTime;
            userdetail.UpdatedDateAndTime = model.UpdatedDateAndTime;
            // Add other properties to update

            // Save the changes to the user
            var result = await userManager.UpdateAsync(userdetail);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User details updated successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Failed to update user details", Errors = result.Errors });
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                First_Name = model.First_Name,
                Last_Name = model.Last_Name,
                Middle_Name = model.Middle_Name,
                IsActive = true,
                PhoneNumber = model.PhoneNumber,
                CreatedDateAndTime = DateTime.Now
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await roleManager.RoleExistsAsync(UserRoles.User))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            //{
            //    await userManager.AddToRoleAsync(user, UserRoles.Admin);
            //}
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        //    [HttpPost]
        //    [Route("register-admin")]
        //    public async Task<IActionResult> RegisterAdmin([FromBody] UserModel model)
        //    {
        //        var userExists = await _userManager.FindByNameAsync(model.First_Name);
        //        if (userExists != null)
        //            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //        ApplicationUser user = new ApplicationUser()
        //        {
        //            Email = model.Email,
        //            SecurityStamp = Guid.NewGuid().ToString(),
        //            UserName = model.First_Name
        //        };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (!result.Succeeded)
        //            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //        {
        //            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        //        }

        //        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //    }

        #region Private Method

        private ApplicationUser ModelToEntity(UserModel user)
        {
            return new ApplicationUser()
            {
                First_Name = user.First_Name,
                Middle_Name = user.Middle_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                PasswordHash = user.Password,
                PhoneNumber = user.Phonenumber,
                IsActive = user.IsActive,
            };
        }

        #endregion
    }
}
