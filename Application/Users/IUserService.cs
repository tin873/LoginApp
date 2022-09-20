using Application.Users.Dto;

namespace Application.Users
{
    public interface IUserService
    {
        Task CreateUser(CreateUserDto createUser);
        Task<LoginResutl> Login(string email, string password);
    }
}
