using Azure.Core;
using InvoiceManager.Data;
using InvoiceManager.DTOs;
using InvoiceManager.Models;
using InvoiceManager.Services;
using InvoiceManager.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.Controllers
{
    /// <summary>
    /// Controller for manage reports
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        IReportsService _reportsService;

        /// <summary>
        /// Constructor for creation of controller
        /// </summary>
        /// <param name="service">Data for control service</param>
        public ReportsController(IReportsService service)
        {
            _reportsService = service;
        }

        /// <summary>
        /// Method for get reports
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of invoices and sum of amounts</returns>
        [HttpGet]
        [Route("ReportOnCustomers")]
        public async Task<ActionResult<string?>> ReportCustomers(ReportRequest request)
        {
            var validator = new ReportsValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            var result = await _reportsService.ReportOnWorks(request);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        /// <summary>
        /// Method for get finished reports
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of invoices and sum of amounts</returns>
        [HttpGet]
        [Route("FinishedReports")]
        public async Task<ActionResult<string?>> FinishedReports(ReportRequest request)
        {
            var validator = new ReportsValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            var result = await _reportsService.ReportOnFinishedWorks(request);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        /// <summary>
        /// Method for get count of statusses
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of statusses</returns>
        [HttpGet]
        [Route("ReportOnStatuses")]
        public async Task<ActionResult<string?>> StatusReport(ReportRequest request)
        {
            var validator = new ReportsValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            var result = await _reportsService.ReportsOnInvoices(request);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}
