using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIDemo.DTO;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _UserManager;

        private readonly IConfiguration _Config;

        public AccountController(UserManager<ApplicationUser> UserManager, IConfiguration config)
        {
            _UserManager = UserManager;
            _Config = config;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO LoginUserDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = await _UserManager.FindByNameAsync(LoginUserDTO.UserName);

                if(AppUser != null)
                {
                    bool found = await _UserManager.CheckPasswordAsync(AppUser, LoginUserDTO.Password);

                    if (found)
                    {
                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.Name, LoginUserDTO.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["JWT:Secret"])); // here we use symmetric key to create only one key for signature and verify

                        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        //create token 

                        JwtSecurityToken newtoken = new JwtSecurityToken(
                            issuer: _Config["JWT:ValidIssuer"], // url web api
                            audience: _Config["JWT:ValidAudience"], // consumer
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials);

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(newtoken) // make toke compact (header,payload,signature)
                            ,
                            expirationdate = newtoken.ValidTo
                        });
                    }

                }
                return Unauthorized();
            }
            return Unauthorized();
        }
    }
}
