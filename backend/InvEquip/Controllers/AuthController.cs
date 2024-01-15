using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;
using InvEquip.Logic.Service;
using InvEquip.Logic.Service.Helper;

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IApplicationAuthService _applicationAuthService;

        public AuthController(IApplicationAuthService applicationAuthService) 
        {
            _applicationAuthService = applicationAuthService;
            
        }

        [HttpPost("Login",Name ="Login")]
        public async Task<ActionResult> LogIn(WebuserLoginDto loginDto)
        {

            ResultStatus result = await _applicationAuthService.CheckUserAuth(loginDto);
            if (result == ResultStatus.NotFound)
            {
                return ApiErrorResponse.ApiResponse.StatusCode404("User Not Exist");
            }
            if (result == ResultStatus.NotAllowed)
            {
                return ApiErrorResponse.ApiResponse.BadRequest("Invalid Credentials");
            }
            WebuserDto userDto = await _applicationAuthService.GetUserByEmail(loginDto.email);
            string JwtTokenString = _applicationAuthService.GenerateJwt(userDto.Id, new JwtService());
            Response.Cookies.Append("Jwt", JwtTokenString, new CookieOptions { HttpOnly = true });

            return Ok(new { message = "Success",});
            
        }
        [HttpPost("register", Name ="register")]
        public async Task<ActionResult> Register(WebuserDto webuserDto)
        {
            webuserDto.Password = BCrypt.Net.BCrypt.HashPassword(webuserDto.Password);
            WebuserDto result = await _applicationAuthService.CreateUser(webuserDto);


            return Created("Success", result);
        }
        [HttpGet("user")]
        public ActionResult WebUser()
        {
            try
            {
                var jwt = Request.Cookies["Jwt"];
                WebuserDto webuserDto = _applicationAuthService.GetUserByJwtCookie(jwt, new JwtService());
                return Ok(webuserDto);

            }catch(Exception )
            {
                return Unauthorized();
            }
        }
        [HttpPost("Logout" , Name ="Logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("Jwt");
            return Ok(new { message = "success" });
        }

    }
}
