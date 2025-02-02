using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserService.Application.DTOs;
using UserService.Application.Services;
using UserService.Domain.Entities;
using UserService.Infrastructure.Database;

namespace UserService.Infrastructure.Services
{
    public class AuthService(UserDbContext dbContext, IConfiguration configuration) : IUserService
    {
        private readonly UserDbContext _dbContext = dbContext;
        private readonly IConfiguration _configuration = configuration;

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                if (user.PasswordHash == HashPassword(password))
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<bool> RegisterUser(string username, string password, string Fullname)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Username == username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(username) || username.Length < 6 || !username.Contains('@'))
            {
                throw new ArgumentException("Username must be at least 6 characters long and contain '@' ");
            }

            if (string.IsNullOrEmpty(password) || password.Length < 8 || !password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                throw new ArgumentException("Password must be at least 8 characters long and contain both letters and numbers.");
            }

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                FullName = Fullname,
                Role = "User"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserInformationDTO> GetUserInformationAsync(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return new UserInformationDTO
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var expirationMinutes = jwtSettings["ExpirationInMinutes"];
            if (!double.TryParse(expirationMinutes, out double expMinutes))
            {
                expMinutes = 60;
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
