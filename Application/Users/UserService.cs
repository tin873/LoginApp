using Application.Users.Dto;
using Core.Exceptions;
using Helper.DI;
using login.EntiryFrameWorkCore.EntityFramWorkCore;
using login.EntiryFrameWorkCore.EntityFramWorkCore.Entity;
using Microsoft.AspNetCore.Identity;

namespace Application.Users
{
    public class UserService : IUserService, ITransient
    {
        private readonly IIdentityCtmDbContext _dbContext;
        private UserManager<User> _userManager;

        public UserService(IIdentityCtmDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
    }
}
