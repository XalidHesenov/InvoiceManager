using InvoiceManager.DTOs;
using InvoiceManager.Models;
using InvoiceManager.Pagination;

namespace InvoiceManager.Services
{
    /// <summary>
    /// Interface for service of customers
    /// </summary>
    public interface IInvoiceService
    {
        /// <summary>
        /// Method for create invoice
        /// </summary>
        /// <param name="invoice">Invoice details</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Created invoice</returns>
        Task<InvoiceGet?> CreateInvoice(InvoiceDto invoice, int userId);

        /// <summary>
        /// Method for edit invoice
        /// </summary>
        /// <param name="id">Id of the invoice to be updated</param>
        /// <param name="invoice">Invoice details</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Updated invoice</returns>
        Task<InvoiceGet?> EditInvoice(int id, InvoiceDto invoice, int userId);

        /// <summary>
        /// Method for change status of invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="statusses">New statutsses of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Updated invoice</returns>
        Task<InvoiceGet?> ChangeInvoiceStatus(int id, InvoiceStatus statusses, int userId);

        /// <summary>
        /// Method for delete invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Deleted invoice</returns>
        Task<InvoiceGet?> DeleteInvoice(int id, int userId);

        /// <summary>
        /// Method for archive invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Archived invoice</returns>
        Task<InvoiceGet?> ArchiveInvoice(int id, int userId);

        /// <summary>
        /// Method for get invoices
        /// </summary>
        /// <param name="page">Count of page</param>
        /// <param name="pageSize">Count of item per page</param>
        /// <param name="search">Search with comment of invoices</param>
        /// <param name="sort">Sort invoices by their total sum</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>List of invoices</returns>
        Task<PaginatedListDto<InvoiceGet?>?> GetInvoicesList(int page, int pageSize, string search, SortEnum sort, int userId);

        /// <summary>
        /// Method for get invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Invoice</returns>
        Task<InvoiceGet?> GetInvoice(int id, int userId);

        /// <summary>
        /// Method for download pdf of invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Pdf file</returns>
        Task<CustomFile?> DownloadInvoice(int id, int userId);
    }
}
