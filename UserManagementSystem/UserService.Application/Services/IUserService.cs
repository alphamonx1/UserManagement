using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Services
{
    public interface IUserService 
    {
        Task<User?> Authenticate(string username, string password);
        Task<bool> RegisterUser(string username, string password , string Fullname);
        Task<UserInformationDTO> GetUserInformationAsync(Guid userId);
        string GenerateJwtToken(User user);
    }
}
