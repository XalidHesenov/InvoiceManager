using InvoiceManager.DTOs;

namespace InvoiceManager.Services
{
    /// <summary>
    /// Interface for service of reports
    /// </summary>
    public interface IReportsService
    {
        /// <summary>
        /// Method for get report on works
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of invoices and total sum</returns>
        public Task<string?> ReportOnWorks(ReportRequest request);

        /// <summary>
        /// Method for get report on finished works 
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of finished invoices and total sum</returns>
        public Task<string?> ReportOnFinishedWorks(ReportRequest request);

        /// <summary>
        /// Method for get invoice statusses count
        /// </summary>
        /// <param name="request">Data for time interval</param>
        /// <returns>Count of invoice statusses</returns>
        public Task<string?> ReportsOnInvoices(ReportRequest request);
    }
}
