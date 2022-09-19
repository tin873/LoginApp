using Application.Users.Dto;

namespace Application.Users
{
    public interface IUserService
    {
        Task CreateUser(CreateUserDto createUser);
    }
}
