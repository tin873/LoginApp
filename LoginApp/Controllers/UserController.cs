using Application.Users;
using Application.Users.Dto;
using Core.Exceptions;
using Core.Model;
using login.EntiryFrameWorkCore.EntityFramWorkCore.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LoginApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : SSControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("CreateUser")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResult))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResult<User>))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                await _userService.CreateUser(createUserDto);
                return Success(createUserDto);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException("Có lỗi xảy ra.");
            }
        }
    }
}
