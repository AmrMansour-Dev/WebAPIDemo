using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.DTO;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _UserManager;

        public AccountController(UserManager<ApplicationUser> UserManager)
        {
            _UserManager = UserManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registeration(RegisterUserDTO userDTO) 
        {
            if(ModelState.IsValid)
            {
                ApplicationUser AppUser = new ApplicationUser();

                AppUser.UserName = userDTO.Username;
                AppUser.Email = userDTO.Email;

               IdentityResult result = await _UserManager.CreateAsync(AppUser, userDTO.Password);
               
                if(result.Succeeded)
                {
                    return Ok("User Created Successfully");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
