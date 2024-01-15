using InvEquip.Configuration;
using InvEquip.Data.Authentication;
using InvEquip.Dto.Requests;
using InvEquip.Dto.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(UserManager<ApplicationUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                //We can utilise the model
                string password = "9Uhdd&430";
                if (user.InviteCode != password)
                {
                    return ApiErrorResponse.ApiResponse.BadRequest("Invalid Invite Code");
                }
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser != null)
                {
                    return ApiErrorResponse.ApiResponse.BadRequest("Email already in use");
                }
                var newUser = new ApplicationUser() { Email = user.Email, UserName = user.Username, FirstName = user.FirstName, LastName = user.LastName };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    Response.Cookies.Append("Jwt2", jwtToken, new CookieOptions { HttpOnly = true, IsEssential = true, SameSite = SameSiteMode.None, Secure = true });

                    return Ok(new RegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken,
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x=>x.Description).ToList(),
                        Success = false,
                        
                    });
                }
            }

            return ApiErrorResponse.ApiResponse.BadRequest("Invalid Payload");
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login (UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                //
                var existinguser = await _userManager.FindByEmailAsync(user.Email);
                if(existinguser == null)
                {
                    return ApiErrorResponse.ApiResponse.BadRequest("Invalid Login Request");
                    //return BadRequest(new RegistrationResponse()
                    //{
                    //    Success = false,
                    //    Errors = new List<string>()
                    //        {
                    //            "Invalid login request"
                    //        }
                    //});
                }
                
                var pwdIsCorrect = await _userManager.CheckPasswordAsync(existinguser, user.Password);
                if (!pwdIsCorrect)
                {
                    return ApiErrorResponse.ApiResponse.BadRequest("Invalid Login Request");
                }

                var jwt = GenerateJwtToken(existinguser);
                Response.Cookies.Append("Jwt2", jwt, new CookieOptions { HttpOnly = true, IsEssential = true, SameSite = SameSiteMode.None, Secure = true, Expires = DateTime.UtcNow.AddHours(6) });
                Response.Cookies.Append("cookietracker", "", new CookieOptions { HttpOnly = false, IsEssential = true, SameSite = SameSiteMode.None, Secure = true, Expires= DateTime.UtcNow.AddHours(6) });
                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = jwt,
                });

                
            }
            return ApiErrorResponse.ApiResponse.BadRequest("Invalid Payload");

        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
        [HttpPost("AuthenticationStatus")]
        
        public ActionResult AuthenticationStatus()
        {
            if (this.User.Identity.IsAuthenticated == true)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [HttpPost("Logout")]
        [Authorize]
        public  void Logout()
        {
            
            Response.Cookies.Delete("Jwt2", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Secure = true,
                Expires = new DateTimeOffset(DateTime.Now),
            });

        }
    }
}
