using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceManager.Data;
using InvoiceManager.Models;
using InvoiceManager.DTOs;
using Microsoft.CodeAnalysis.Scripting;
using BCrypt.Net;
using InvoiceManager.DTOs.Auth;
using InvoiceManager.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using InvoiceManager.Validation;

namespace InvoiceManager.Controllers
{
    /// <summary>
    /// Controller for manage account
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        /// <summary>
        /// Constructor for creation of controller
        /// </summary>
        /// <param name="service">Data for control service</param>
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        /// <summary>
        /// Method for register
        /// </summary>
        /// <param name="user">User information</param>
        /// <returns>Token for authorization</returns>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<AuthTokenDto?>> Register(UserDto user)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            };
            var registerResult = await _userService.Register(user);
            if (registerResult == null)
            {
                return BadRequest("Username already exists");
            }
            var token = await _userService.Login(registerResult);
            return token;
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="request">Login informations</param>
        /// <returns>Token for authorization</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthTokenDto?>> Login(LoginRequest request)
        {
            var token = await _userService.Login(request);
            if (token == null)
            {
                return BadRequest("Username or password is incorrect");
            }
            return token;
        }
        
        /// <summary>
        /// Method for update account informations
        /// </summary>
        /// <param name="user">New user informations</param>
        /// <returns>Updated account</returns>
        [Authorize]
        [HttpPut]
        [Route("EditProfile")]
        public async Task<ActionResult<User?>> EditProfile(UserDto user)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            };
            int id = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _userService.EditProfile(id, user);
            if (result.Id == 0)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        /// <summary>
        /// Method for change password
        /// </summary>
        /// <param name="request">Informations for change password</param>
        /// <returns>Updated account</returns>
        [Authorize]
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<ActionResult<User?>> ChangePassword(PasswordChangeRequest request)
        {
            var validator = new PasswordChangeRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            };
            int id = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _userService.ChangePassword(id, request);
            if (result == null)
            {
                return BadRequest("Old password is wrong");
            }
            if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for delete account
        /// </summary>
        /// <returns>Deleted account</returns>
        [Authorize]
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult<User?>> DeleteUser()
        {
            int id = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _userService.DeleteProfile(id);
            if (result == null)
            {
                return Unauthorized();
            }
            return result;
        }
    }
}
