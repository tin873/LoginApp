using Application.Users.Dto;
using Core.Exceptions;
using Helper.DI;
using login.EntiryFrameWorkCore.EntityFramWorkCore;
using login.EntiryFrameWorkCore.EntityFramWorkCore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users
{
    public class UserService : IUserService, ITransient
    {
        private readonly IIdentityCtmDbContext _dbContext;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;
        public UserService(IIdentityCtmDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        public async Task CreateUser(CreateUserDto createUser)
        {
            var result = await _userManager.FindByNameAsync(createUser.UserName);
            if (result != null)
            {
                throw new UserFriendlyException("Tên tài khoản đã tồn tại");
            }
            else
            {
                var user = new User() { Email = createUser.Email, UserName = createUser.UserName };
                var createUsers = await _userManager.CreateAsync(user, createUser.Password);
                if (!createUsers.Succeeded)
                {
                    throw new UserFriendlyException("Tạo tài khoản thất bại.");
                }
            }
        }

        public async Task<LoginResutl> Login(string email, string password)
        {
            var checkExitsEmail = await _userManager.FindByEmailAsync(email);
            if(checkExitsEmail == null)
            {
                throw new UserFriendlyException("Tên tài khoản không tồn tại");
            }
            var checkExitsPassword = await _userManager.CheckPasswordAsync(checkExitsEmail, password);
            if (!checkExitsPassword)
            {
                throw new UserFriendlyException("Sai mật khẩu");
            }
            var role = _userManager.GetRolesAsync(checkExitsEmail);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, checkExitsEmail.Email),
                new Claim(ClaimTypes.GivenName, checkExitsEmail.UserName),
                new Claim(ClaimTypes.Role, string.Join(";", role))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens: Issuer"], _config["Tokens: Issuer"], claims, expires: DateTime.Now.AddHours(1), signingCredentials: creds);
            var result = new LoginResutl();
            result.token = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
    }
}
