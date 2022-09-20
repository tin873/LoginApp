using Application.Users;
using Application.Users.Dto;
using Core.Exceptions;
using Core.Model;
using login.EntiryFrameWorkCore.EntityFramWorkCore.Entity;
using LoginApp.ModelView;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LoginApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : SSControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResult))]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResult<User>))]
        public async Task<LoginResutl> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                return await _userService.Login(loginRequest.Email, loginRequest.Password);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Có lỗi xảy ra.");
            }
        }
    }
}
