using InvoiceManager.DTOs;
using InvoiceManager.Models;
using InvoiceManager.Pagination;

namespace InvoiceManager.Services
{
    /// <summary>
    /// Interface for service of customers
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Method for create customer
        /// </summary>
        /// <param name="customer">Customer's details for create customer</param>
        /// <param name="userId">Id for customer's user</param>
        /// <returns>Customer</returns>
        Task<CustomerGet?> CreateCustomer(CustomerDto customer, int userId);

        /// <summary>
        /// Method for edit Customer
        /// </summary>
        /// <param name="id">Id of the customer to be updated</param>
        /// <param name="userId">Id of user for authorization</param>
        /// <param name="customer">Details of edited customer</param>
        /// <returns>Customer</returns>
        Task<CustomerGet?> EditCustomer(int id, int userId, CustomerDto customer);

        /// <summary>
        /// Method for delete customer
        /// </summary>
        /// <param name="id">Id of the customer to be deleted</param>
        /// <param name="userId">Id of the user for authorization</param>
        /// <returns>Customer</returns>
        Task<CustomerGet?> DeleteCustomer(int id, int userId);

        /// <summary>
        /// Method for archive customer
        /// </summary>
        /// <param name="id">Id of the customer to be archived</param>
        /// <param name="userId">Users's id of the customer to be archived</param>
        /// <returns>Customer</returns>
        Task<CustomerGet?> ArchiveCustomer(int id, int userId);

        /// <summary>
        /// Method for get customers
        /// </summary>
        /// <param name="page">Count of page</param>
        /// <param name="pageSize">Count of item per page</param>
        /// <param name="search">Search with names of customers</param>
        /// <param name="sortEnum">Sort customer by their addresses</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>List of customers</returns>
        Task<PaginatedListDto<CustomerGet?>?> GetCustomerList(int page, int pageSize, string? search, SortEnum sortEnum, int userId);

        /// <summary>
        /// Method for get customer
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Customer</returns>
        Task<CustomerGet?> GetCustomer(int id, int userId);
    }
}
