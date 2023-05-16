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
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Microsoft.AspNetCore.Authorization;
using InvoiceManager.Services;
using InvoiceManager.Pagination;
using static NuGet.Packaging.PackagingConstants;
using System.Text.RegularExpressions;
using InvoiceManager.Validation;

namespace InvoiceManager.Controllers
{
    /// <summary>
    /// Controller for manage invoices
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        IInvoiceService _invoiceService;

        /// <summary>
        /// Constructor for creation of controller
        /// </summary>
        /// <param name="service">Data for control service</param>
        public InvoicesController(IInvoiceService service)
        {
            _invoiceService = service;
        }

        /// <summary>
        /// Method for create invoice
        /// </summary>
        /// <param name="invoice">Invoice informations</param>
        /// <returns>Created invoice</returns>
        [HttpPost]
        [Route("CreateInvoice")]
        public async Task<ActionResult<InvoiceGet?>> CreateInvoice(InvoiceDto invoice)
        {
            var validator = new InvoiceValidator();
            var validationResult = validator.Validate(invoice);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result =  await _invoiceService.CreateInvoice(invoice, userId);
            if (result is null)
            {
                return BadRequest("You don't have an customer with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for edit invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="invoice">New informations of invoice</param>
        /// <returns>Updated invoice</returns>
        [HttpPut]
        [Route("EditInvoiceById")]
        public async Task<ActionResult<InvoiceGet?>> EditInvoice(int id, InvoiceDto invoice)
        {
            var validator = new InvoiceValidator();
            var validationResult = validator.Validate(invoice);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _invoiceService.EditInvoice(id, invoice, userId);
            if (result == null)
            {
                return BadRequest("You don't have an invoice with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            else if (result.Id == -1)
            {
                return BadRequest("This invoice has been sent so you can't edit it");
            }
            return result;
        }
        
        /// <summary>
        /// Method for change status of invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="statusses">New status of invoice</param>
        /// <returns>Updated invoice</returns>
        [HttpPut]
        [Route("ChangeInvoiceStatusById")]
        public async Task<ActionResult<InvoiceGet?>> ChangeInvoiceStatus(int id, InvoiceStatus statusses)
        {
            var validator = new InvoiceStatusValidator();
            var validationResult = validator.Validate(statusses);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _invoiceService.ChangeInvoiceStatus(id, statusses, userId);
            if (result == null)
            {
                return BadRequest("You don't have an invoice with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }
        
        /// <summary>
        /// Method for delete invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <returns>Deleted invoice</returns>
        [HttpDelete]
        [Route("DeleteInvoiceById")]
        public async Task<ActionResult<InvoiceGet?>> DeleteInvoice(int id)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _invoiceService.DeleteInvoice(id, userId);
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
                return BadRequest("This invoice has been sent so you can't delete it");
            }
            return result;
        }

        /// <summary>
        /// Method for archive invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <returns>Archived invoice</returns>
        [HttpDelete]
        [Route("ArchiveInvoiceById")]
        public async Task<ActionResult<InvoiceGet?>> ArchiveInvoice(int id)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _invoiceService.ArchiveInvoice(id, userId);
            if (result == null)
            {
                return BadRequest("You don't have a customer with this id");
            }
            if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for get invoices
        /// </summary>
        /// <param name="filters">Data for filter invoices</param>
        /// <param name="pagination">Data for paginating invoices</param>
        /// <returns>User's customer's invoices</returns>
        [HttpGet]
        [Route("GetInvoices")]
        public async Task<ActionResult<PaginatedListDto<InvoiceGet?>?>> GetInvoices([FromQuery] InvoiceFilters filters, [FromQuery] PaginationRequest pagination)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _invoiceService.GetInvoicesList(pagination.Page, pagination.PageSize, filters.Search, filters.Enum, userId);
            if (result == null)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for get invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <returns>Invoice</returns>
        [HttpGet]
        [Route("GetInvoiceById")]
        public async Task<ActionResult<InvoiceGet?>> GetInvoice(int id)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            var result = await _invoiceService.GetInvoice(id, userId);
            if (result == null)
            {
                return BadRequest("You don't have an invoice with this id");
            }
            else if (result.Id == 0)
            {
                return Unauthorized();
            }
            return result;
        }

        /// <summary>
        /// Method for download invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <returns>Pdf file of invoice</returns>
        [HttpGet]
        [Route("DownloadInvoiceById")]
        public async Task<ActionResult<Invoice?>> DownloadInvoice(int id)
        {
            int userId = int.Parse(User.Claims.Where(c => c.Type == "my_own_user_id_claim").FirstOrDefault().Value);
            CustomFile? result = await _invoiceService.DownloadInvoice(id, userId);
            if (result == null)
            {
                return BadRequest("You don't have an invoice with this id");
            }
            else if (result.FileName == "unauth")
            {
                return Unauthorized();
            }
            return File(result.fileStream, result.ContentType, result.FileName);
        }
    }
}
