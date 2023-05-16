using InvoiceManager.Data;
using InvoiceManager.DTOs;
using InvoiceManager.DTOs.Auth;
using InvoiceManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoiceManager.Services
{
    /// <summary>
    /// Service for control users
    /// </summary>
    public class UserService : IUserService
    {
        InvoiceContext _dbContext;

        /// <summary>
        /// Constructor for create serivce
        /// </summary>
        /// <param name="dbContext">Data for control database</param>
        public UserService(InvoiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for change user's password
        /// </summary>
        /// <param name="id">Current user's id for authorization</param>
        /// <param name="request">Password details</param>
        /// <returns>Updated user</returns>
        public async Task<User?> ChangePassword(int id, PasswordChangeRequest request)
        {
            var oldUser = _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (oldUser == null) { return new User(); }
            if (!BCrypt.Net.BCrypt.Verify(request.currentPassword, oldUser.Password))
            {
                return null;
            }
            request.NewPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            oldUser.Password = request.NewPassword;
            await _dbContext.SaveChangesAsync();
            return oldUser;
        }

        /// <summary>
        /// Method for delete user's account
        /// </summary>
        /// <param name="id">Current user's id for authorization</param>
        /// <returns>Deleted user</returns>
        public async Task<User?> DeleteProfile(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Method for edit profile details
        /// </summary>
        /// <param name="id">Current user's id for authorization</param>
        /// <param name="user">User's new informations</param>
        /// <returns>Updated user</returns>
        public async Task<User?> EditProfile(int id, UserDto user)
        {
            var oldUser = _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (oldUser == null)
            {
                return new User();
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            oldUser.Username = user.Username;
            oldUser.Password = user.Password;
            oldUser.Address = user.Address;
            oldUser.Email = user.Email;
            oldUser.PhoneNumber = user.PhoneNumber;
            oldUser.UpdatedAt = DateTimeOffset.Now;
            await _dbContext.SaveChangesAsync();
            return oldUser;
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="request">User's informations for authentication</param>
        /// <returns>Token</returns>
        public async Task<AuthTokenDto?> Login(LoginRequest request)
        {
            var user = await _dbContext.Users.Where(u => u.Username == request.Username).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }
            var claims = new[]
            {   
                new Claim(ClaimsIdentity.DefaultNameClaimType, "admin"),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"),
                new Claim("my_own_user_id_claim", $"{user.Id}"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Super Sequrity Key"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:5000",
                audience: "https://localhost:5000",            
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials,
                claims: claims
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return new AuthTokenDto
                {
                    Token = tokenValue
                };
        }

        /// <summary>
        /// Method for register
        /// </summary>
        /// <param name="user">User's informations</param>
        /// <returns>User's details</returns>
        public async Task<LoginRequest?> Register(UserDto user)
        {
            var DbUser = _dbContext.Users.Where(u => u.Username == user.Username).FirstOrDefault();
            if (DbUser != null)
            {
                return null;
            }
            var newPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            User newUser = new User()
            {
                Id = default,
                Username = user.Username,
                Password = newPassword,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return new LoginRequest() { Username = user.Username, Password = user.Password};
        }
    }
}
