using InvoiceManager.Data;
using InvoiceManager.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Services
{
    /// <summary>
    /// Service for reports
    /// </summary>
    public class ReportsService : IReportsService
    {
        InvoiceContext _dbContext;
        /// <summary>
        /// Constructor for create service
        /// </summary>
        /// <param name="dbContext">Data for control database</param>
        public ReportsService(InvoiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for get report on works
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of invoices and total sum</returns>
        public async Task<string?> ReportOnWorks(ReportRequest request)
        {
            var invoices = _dbContext.Invoices.Where(i => i.StartDate >= request.startOfInterval && i.EndDate <= request.endOfInterval);
            if (invoices == null)
            {
                return null;
            }
            var count = await invoices.CountAsync();
            var totalSum = await invoices.SumAsync(i => i.TotalSum);
            return $"Count of invoices : {count}\nTotal amount of invoices : {totalSum}";
        }

        /// <summary>
        /// Method for get report on finished works 
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of finished invoices and total sum</returns>
        public async Task<string?> ReportOnFinishedWorks(ReportRequest request)
        {
            var invoices = _dbContext.Invoices.Where(i => i.StartDate >= request.startOfInterval && i.EndDate <= request.endOfInterval && i.IsPaid);
            if (invoices == null)
            {
                return null;
            }
            var count = await invoices.CountAsync();
            var totalSum = await invoices.SumAsync(i => i.TotalSum);
            return $"Count of invoices : {count}\nTotal amount of invoices : {totalSum}";   
        }

        /// <summary>
        /// Method for get invoice statusses count
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of invoice statusses</returns>
        public async Task<string?> ReportsOnInvoices(ReportRequest request)
        {
            var invoices = _dbContext.Invoices.Where(i => i.StartDate >= request.startOfInterval && i.EndDate <= request.endOfInterval);
            if (invoices == null)
            {
                return null;
            }
            int IsCreatedCount = await invoices.Where(i => i.IsCreated).CountAsync();
            int IsSentCount = await invoices.Where(i => i.IsSent).CountAsync();
            int IsReceviedCount = await invoices.Where(i => i.IsReceived).CountAsync();
            int IsPaidCount = await invoices.Where(i => i.IsPaid).CountAsync();
            int IsCancelledCount = await invoices.Where(i => i.IsCancelled).CountAsync();
            int IsRejectedCount = await invoices.Where(i => i.IsRejected).CountAsync();
            return $"IsCreated count : {IsCreatedCount}" +
                $"\nIsSent count : {IsSentCount}" +
                $"\nIsReceived count : {IsReceviedCount}" +
                $"\nIsPaid count : {IsPaidCount}" +
                $"\nIsCancelled count : {IsCancelledCount}" +
                $"\nIsRejected count : {IsRejectedCount}";
        }
    }
}
