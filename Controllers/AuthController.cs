using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenReposity tokenReposity;

        public AuthController(UserManager<IdentityUser> userManager, ITokenReposity tokenReposity)
        {
            this.userManager = userManager;
            this.tokenReposity = tokenReposity;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Registered! Please Login");
                    }
                }

            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);

            if (user != null)
            {
                var CheckPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if(CheckPasswordResult)
                {
                    //get roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //create Token here
                        var jwtToken = tokenReposity.CreateJWTToken(user, roles.ToList());
                        var Respose = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(jwtToken);

                    }
                                        
                }
            }
            return BadRequest("UserName and Password is Incorrect");
        }

    }
}