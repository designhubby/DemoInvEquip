using InvEquip.Data.Authentication;
using InvEquip.Dto.Requests;
using InvEquip.Dto.Responses;
using InvEquip.Logic.Service.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InvEquip.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<UserInfo>  Get()
        {
            var user = await _userManager.FindByIdAsync(this.User.GetIdentityId());
            
            return new UserInfo()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName  = user.LastName,
                Claims = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value),
            };
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangeUserPassword(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (!ModelState.IsValid)
            {
                return ApiErrorResponse.ApiResponse.BadRequest("Required Fields Unfilled");
            }
            var user = await _userManager.FindByIdAsync(this.User.GetIdentityId());
            var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPwd, userPasswordChangeDto.NewPwd);
            return Ok();
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(UserDataChangeDto userDataChangeDto)
        {
            if (!ModelState.IsValid)
            {
                return ApiErrorResponse.ApiResponse.BadRequest("Invalid Request");
            }
            var user = await _userManager.FindByIdAsync(this.User.GetIdentityId());

            user.LastName = userDataChangeDto.LastName;
            user.FirstName = userDataChangeDto.FirstName;
            user.UserName = userDataChangeDto.Username;
            user.Email = userDataChangeDto.Email;
            user.Id = userDataChangeDto.Id;

            IdentityResult result = await _userManager.UpdateAsync(user);
            bool successState = result.Succeeded;

            return successState == true ?  Ok(successState) :  ApiErrorResponse.ApiResponse.StatusCode500();
            
        }
    }

}
