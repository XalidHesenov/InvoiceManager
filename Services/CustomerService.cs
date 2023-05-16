using InvoiceManager.Data;
using InvoiceManager.DTOs;
using InvoiceManager.Models;
using InvoiceManager.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
namespace InvoiceManager.Services
{
    /// <summary>
    /// Service for customers
    /// </summary>
    public class CustomerService : ICustomerService
    {
        InvoiceContext _dbContext;

        /// <summary>
        /// Constructor for creation of customer
        /// </summary>
        /// <param name="dbContext">Data for control database</param>
        public CustomerService(InvoiceContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Method for archive customer
        /// </summary>
        /// <param name="id">Id of the customer to be archived</param>
        /// <param name="userId">Users's id of the customer to be archived</param>
        /// <returns>Customer</returns>
        public async Task<CustomerGet?> ArchiveCustomer(int id, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return new CustomerGet();
            }
            Customer? customer = await _dbContext.Customers.Where(c => c.Id == id && c.UserId == user.Id).FirstOrDefaultAsync();
            if (customer == null)
            {
                return null;
            }
            customer.DeletedAt = DateTimeOffset.Now;
            await _dbContext.SaveChangesAsync();
            var returnValue = new CustomerGet()
            {
                Id = customer.Id,
                Name = customer.Name,
                Password = customer.Password,
                Address = customer.Address,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = DateTimeOffset.Now,
            };
            return returnValue;
        }

        /// <summary>
        /// Method for create customer
        /// </summary>
        /// <param name="customer">Customer's details for create customer</param>
        /// <param name="userId">Id for customer's user</param>
        /// <returns>Customer</returns>
        public async Task<CustomerGet?> CreateCustomer(CustomerDto customer, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            var newCustomer = new Customer
            {
                Id = default,
                Name = customer.Name,
                Password = customer.Password,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
                UserId = user.Id,
                User = user,
                DeletedAt = null
            };
            _dbContext.Customers.Add(newCustomer);
            await _dbContext.SaveChangesAsync();
            var returnValue = new CustomerGet()
            {
                Id = newCustomer.Id,
                Name = newCustomer.Name,
                Password = newCustomer.Password,
                Address = newCustomer.Address,
                Email = newCustomer.Email,
                PhoneNumber = newCustomer.PhoneNumber,
                CreatedAt = newCustomer.CreatedAt,
                UpdatedAt = newCustomer.UpdatedAt,
            };
            return returnValue;
        }

        /// <summary>
        /// Method for delete customer
        /// </summary>
        /// <param name="id">Id of the customer to be deleted</param>
        /// <param name="userId">Id of the user for authorization</param>
        /// <returns>Customer</returns>
        public async Task<CustomerGet?> DeleteCustomer(int id, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return new CustomerGet();
            }
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null || customer.UserId != user.Id)
            {
                return null;
            }
            var list = await _dbContext.Invoices.Where(i => i.CustomerId == customer.Id).ToListAsync();
            if (list.Count != 0)
            {
                return new CustomerGet() { Id = -1};
            }
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
            var returnValue = new CustomerGet()
            {
                Id = customer.Id,
                Name = customer.Name,
                Password = customer.Password,
                Address = customer.Address,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = DateTimeOffset.Now,
            };
            return returnValue;
        }

        /// <summary>
        /// Method for edit Customer
        /// </summary>
        /// <param name="id">Id of the customer to be updated</param>
        /// <param name="userId">Id of user for authorization</param>
        /// <param name="customer">Details of edited customer</param>
        /// <returns>Customer</returns>
        public async Task<CustomerGet?> EditCustomer(int id, int userId, CustomerDto customer)
        {
            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return new CustomerGet();
            }
            var oldCustomer = _dbContext.Customers.Where(c => c.Id == id && c.UserId == user.Id).FirstOrDefault();
            if (oldCustomer == null)
            {
                return null;
            }
            oldCustomer.Name = customer.Name;
            oldCustomer.Password = customer.Password;
            oldCustomer.Email = customer.Email;
            oldCustomer.PhoneNumber = customer.PhoneNumber;
            oldCustomer.Address = customer.Address;
            oldCustomer.UpdatedAt = DateTimeOffset.Now;
            await _dbContext.SaveChangesAsync();
            var returnValue = new CustomerGet()
            {
                Id = oldCustomer.Id,
                Name = oldCustomer.Name,
                Password = oldCustomer.Password,
                Address = oldCustomer.Address,
                Email = oldCustomer.Email,
                PhoneNumber = oldCustomer.PhoneNumber,
                CreatedAt = oldCustomer.CreatedAt,
                UpdatedAt = DateTimeOffset.Now,
            };
            return returnValue;
        }

        /// <summary>
        /// Method for get customer
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Customer</returns>
        public async Task<CustomerGet?> GetCustomer(int id, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return new CustomerGet();
            }
            var customer = await _dbContext.Customers.Where(c => c.Id == id && c.UserId == userId).FirstOrDefaultAsync();
            if (customer == null)
            {
                return null;
            }
            var returnValue = new CustomerGet()
            {
                Id = customer.Id,
                Name = customer.Name,
                Password = customer.Password,
                Address = customer.Address,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt,
            };
            return returnValue;
        }

        /// <summary>
        /// Method for get customers
        /// </summary>
        /// <param name="page">Count of page</param>
        /// <param name="pageSize">Count of item per page</param>
        /// <param name="search">Search with names of customers</param>
        /// <param name="sort">Sort customer by their addresses</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>List of customers</returns>
        public async Task<PaginatedListDto<CustomerGet?>?> GetCustomerList(int page, int pageSize, string? search, SortEnum sort, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            IQueryable<Customer> query = _dbContext.Customers.Where(c => c.UserId == userId);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }
            if (sort == SortEnum.ASC)
            {
                query = query.OrderBy(t => t.Address);
            }
            else if (sort == SortEnum.DESC)
            {
                query = query.OrderByDescending(t => t.Address);
            }
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PaginatedListDto<CustomerGet?>(
                items.Select(t => new CustomerGet
                {
                    Id = t.Id,
                    Name = t.Name,
                    Address = t.Address,
                    Email = t.Email,
                    Password = t.Password,
                    PhoneNumber = t.PhoneNumber,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                }),
                new PaginationMeta(page, pageSize, totalCount)
                );
        }
    }
}
