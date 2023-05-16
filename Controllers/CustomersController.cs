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
using InvoiceManager.Pagination;
using InvoiceManager.Services;
using Microsoft.AspNetCore.Authorization;
using InvoiceManager.Validation;

namespace InvoiceManager.Controllers
{
    /// <summary>
    /// Controller for manage customers
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _cutomerService;

        /// <summary>
        /// Constructor for creation of controller
        /// </summary>
        /// <param name="service">Data for control service</param>
        public CustomersController(ICustomerService service)
        {
            _cutomerService = service;
        }

        /// <summary>
        /// Method for get customers
        /// </summary>
        /// <param name="filters">Data for filter customers</param>
        /// <param name="pagination">Data for paginating customers</param>
        /// <returns>User's customers</returns>
        //GET: api/Customers
       [HttpGet]
       [Route("GetCustomers")]
        public async Task<ActionResult<PaginatedListDto<CustomerGet?>?>> GetCustomers([FromQuery] CustomerFilters filters, [FromQuery] PaginationRequest pagination)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _cutomerService.GetCustomerList(pagination.Page, pagination.PageSize, filters.Search, filters.Enum, userId);
            if (result == null)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for get customer
        /// </summary>
        /// <param name="id">Customer's id</param>
        /// <returns>Customer</returns>
        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<ActionResult<CustomerGet?>> GetCustomerById(int id)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _cutomerService.GetCustomer(id, userId);
            if (result == null)
            {
                return BadRequest("You don't have a customer with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for add customer
        /// </summary>
        /// <param name="customer">Customer informations</param>
        /// <returns>Added customer</returns>
        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult<CustomerGet?>> AddCustomer(CustomerDto customer)
        {
            var validator = new CustomerValidator();
            var validationResult = validator.Validate(customer);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _cutomerService.CreateCustomer(customer, userId);
            if (result == null)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for edit customer
        /// </summary>
        /// <param name="id">Customer's id</param>
        /// <param name="customer">Customer's new informations</param>
        /// <returns>Updated customer</returns>
        [HttpPut]
        [Route("EditCustomerById")]
        public async Task<ActionResult<CustomerGet?>> EditCustomerById(int id, CustomerDto customer)
        {
            var validator = new CustomerValidator();
            var validationResult = validator.Validate(customer);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _cutomerService.EditCustomer(id, userId, customer);
            if (result == null)
            {
                return BadRequest("You don't have a customer with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }


        /// <summary>
        /// Method for delete customer
        /// </summary>
        /// <param name="id">Customer's id</param>
        /// <returns>Deleted customer</returns>
        [HttpDelete]
        [Route("DeleteCustomerById")]
        public async Task<ActionResult<CustomerGet?>> DeleteCustomer(int id)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _cutomerService.DeleteCustomer(id, userId);
            if (result == null)
            {
                return BadRequest("You don't have a customer with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            else if (result.Id == -1)
            {
                return BadRequest("This customer sent the invoice so you can't delete it");
            }
            return result;
        }
        
        /// <summary>
        /// Method for archive customer
        /// </summary>
        /// <param name="id">Customer's id</param>
        /// <returns>Archived customer</returns>
        [HttpDelete]
        [Route("ArchiveCustomerById")]
        public async Task<ActionResult<CustomerGet?>> ArchiveCustomer(int id)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _cutomerService.ArchiveCustomer(id, userId);
            if (result == null)
            {
                return BadRequest("You don't have a customer with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }
    }
}
