using InvoiceManager.DTOs;
using InvoiceManager.DTOs.Auth;
using InvoiceManager.Models;

namespace InvoiceManager.Services
{
    /// <summary>
    /// Interface for service of users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Method for register
        /// </summary>
        /// <param name="user">User's informations</param>
        /// <returns>User's details</returns>
        Task<LoginRequest?> Register(UserDto user);

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="request">User's informations for authentication</param>
        /// <returns>Token</returns>
        Task<AuthTokenDto?> Login(LoginRequest request);

        /// <summary>
        /// Method for edit profile details
        /// </summary>
        /// <param name="id">Current user's id for authorization</param>
        /// <param name="user">User's new informations</param>
        /// <returns>Updated user</returns>
        Task<User?> EditProfile(int id, UserDto user);

        /// <summary>
        /// Method for change user's password
        /// </summary>
        /// <param name="id">Current user's id for authorization</param>
        /// <param name="request">Password details</param>
        /// <returns>Updated user</returns>
        Task<User?> ChangePassword(int id, PasswordChangeRequest request);

        /// <summary>
        /// Method for delete user's account
        /// </summary>
        /// <param name="id">Current user's id for authorization</param>
        /// <returns>Deleted user</returns>
        Task<User?> DeleteProfile(int id);
    }
}
