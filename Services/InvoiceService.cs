using InvoiceManager.Data;
using InvoiceManager.DTOs;
using InvoiceManager.Models;
using InvoiceManager.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InvoiceManager.Services
{
    /// <summary>
    /// Service for control invoices
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        InvoiceContext _dbContext;
        /// <summary>
        /// Constructor for create service
        /// </summary>
        /// <param name="dbContext">Data for control database</param>
        public InvoiceService(InvoiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for archive invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Archived invoice</returns>
        public async Task<InvoiceGet?> ArchiveInvoice(int id, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return new InvoiceGet();
            }
            var invoice = await _dbContext.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return null;
            }
            var customer = await _dbContext.Customers.Where(c => c.UserId == user.Id).FirstOrDefaultAsync();
            if (customer == null || customer.Id != invoice.CustomerId)
            {
                return null;
            }
            invoice.DeletedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            List<InvoiceRow> rowsInvoice = _dbContext.InvoiceRows.Where(ir => ir.InvoiceId == invoice.Id).ToList();
            List<InvoiceRowGet> resultRows = new();
            foreach (var row in rowsInvoice)
            {
                var item = new InvoiceRowGet()
                {
                    Id = row.Id,
                    Service = row.Service,
                    Sum = row.Sum,
                    Amount = row.Amount,
                    Quantity = row.Quantity,
                };
                resultRows.Add(item);
            }
            var returnResult = new InvoiceGet()
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                StartDate = invoice.StartDate,
                EndDate = invoice.EndDate,
                TotalSum = invoice.TotalSum,
                Comment = invoice.Comment,
                CreatedAt = invoice.CreatedAt,
                IsCancelled = invoice.IsCancelled,
                IsCreated = invoice.IsCreated,
                IsPaid = invoice.IsPaid,
                IsReceived = invoice.IsReceived,
                IsRejected = invoice.IsRejected,
                IsSent = invoice.IsSent,
                Rows = resultRows
            };
            return returnResult;
        }

        /// <summary>
        /// Method for change status of invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="statusses">New statutsses of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Updated invoice</returns>
        public async Task<InvoiceGet?> ChangeInvoiceStatus(int id, InvoiceStatus statusses, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return new InvoiceGet();
            }
            var invoice = await _dbContext.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return null;
            }
            var customer = await _dbContext.Customers.Where(c => c.UserId == user.Id).FirstOrDefaultAsync();
            if (customer == null || customer.Id != invoice.CustomerId)
            {
                return null;
            }
            invoice.IsCreated = statusses.IsCreated;
            invoice.IsRejected = statusses.IsRejected;
            invoice.IsReceived = statusses.IsReceived;
            invoice.IsPaid = statusses.IsPaid;
            invoice.IsSent = statusses.IsSent;
            invoice.IsCancelled = statusses.IsCancelled;
            await _dbContext.SaveChangesAsync();

            List<InvoiceRow> rows = _dbContext.InvoiceRows.Where(ir => ir.InvoiceId == invoice.Id).ToList();
            List<InvoiceRowGet> resultRows = new();
            foreach (var row in rows)
            {
                var item = new InvoiceRowGet()
                {
                    Id = row.Id,
                    Service = row.Service,
                    Sum = row.Sum,
                    Amount = row.Amount,
                    Quantity = row.Quantity,
                };
                resultRows.Add(item);
            }
            var returnResult = new InvoiceGet()
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                StartDate = invoice.StartDate,
                EndDate = invoice.EndDate,
                TotalSum = invoice.TotalSum,
                Comment = invoice.Comment,
                CreatedAt = invoice.CreatedAt,
                IsCancelled = invoice.IsCancelled,
                IsCreated = invoice.IsCreated,
                IsPaid = invoice.IsPaid,
                IsReceived = invoice.IsReceived,
                IsRejected = invoice.IsRejected,
                IsSent = invoice.IsSent,
                Rows = resultRows
            };
            return returnResult;
        }

        /// <summary>
        /// Method for create invoice
        /// </summary>
        /// <param name="invoice">Invoice details</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Created invoice</returns>
        public async Task<InvoiceGet?> CreateInvoice(InvoiceDto invoice, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return new InvoiceGet();
            }
            var customer = await _dbContext.Customers.FindAsync(invoice.CustomerId);
            if (customer == null || customer.UserId != user.Id)
            {
                return null;
            }
            decimal total = 0;
            foreach (var item in invoice.InvoiceRows)
            {
                total += item.Amount * item.Quantity;
            }
            Invoice newInvoice = new Invoice()
            {
                Id = default,
                CustomerId = invoice.CustomerId,
                StartDate = invoice.StartDate,
                EndDate = invoice.EndDate,
                TotalSum = total,
                Comment = invoice.Comment,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
                DeletedAt = null,
                IsCreated = true,
            };
            _dbContext.Invoices.Add(newInvoice);
            await _dbContext.SaveChangesAsync();
            List<InvoiceRow> rowList = new List<InvoiceRow>();
            foreach (var item in invoice.InvoiceRows)
            {
                var newInvoiceRow = new InvoiceRow()
                {
                    Id = default,
                    InvoiceId = newInvoice.Id,
                    Service = item.Service,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    Sum = item.Amount * item.Quantity
                };
                _dbContext.InvoiceRows.Add(newInvoiceRow);
                rowList.Add(newInvoiceRow);
            }
            await _dbContext.SaveChangesAsync();

            List<InvoiceRowGet> resultRows = new();
            foreach (var row in rowList)
            {
                var item = new InvoiceRowGet()
                {
                    Id = row.Id,
                    Service = row.Service,
                    Sum = row.Sum,
                    Amount = row.Amount,
                    Quantity = row.Quantity,
                };
                resultRows.Add(item);
            }
            var returnResult = new InvoiceGet()
            {
                Id = newInvoice.Id,
                CustomerId = newInvoice.CustomerId,
                StartDate = newInvoice.StartDate,
                EndDate = newInvoice.EndDate,
                TotalSum = newInvoice.TotalSum,
                Comment = newInvoice.Comment,
                CreatedAt = newInvoice.CreatedAt,
                IsCancelled = newInvoice.IsCancelled,
                IsCreated = newInvoice.IsCreated,
                IsPaid = newInvoice.IsPaid,
                IsReceived = newInvoice.IsReceived,
                IsRejected = newInvoice.IsRejected,
                IsSent = newInvoice.IsSent,
                Rows = resultRows
            };
            return returnResult;
        }

        /// <summary>
        /// Method for delete invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Deleted invoice</returns>
        public async Task<InvoiceGet?> DeleteInvoice(int id, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return new InvoiceGet();
            }
            var invoice = await _dbContext.Invoices.FindAsync(id);
            if (invoice is null)
            {
                return null;
            }
            var customer = await _dbContext.Customers.FindAsync(invoice.CustomerId);
            if (customer == null || customer.UserId != user.Id)
            {
                return null;
            }
            if (invoice.IsSent == true)
            {
                return new InvoiceGet() { Id = -1 };
            }
            var rows = _dbContext.InvoiceRows.Where(ir => ir.InvoiceId == invoice.Id).ToList();
            _dbContext.Invoices.Remove(invoice);
            _dbContext.InvoiceRows.RemoveRange(rows);
            await _dbContext.SaveChangesAsync();

            List<InvoiceRowGet> resultRows = new();
            foreach (var row in rows)
            {
                var item = new InvoiceRowGet()
                {
                    Id = row.Id,
                    Service = row.Service,
                    Sum = row.Sum,
                    Amount = row.Amount,
                    Quantity = row.Quantity,
                };
                resultRows.Add(item);
            }
            var returnResult = new InvoiceGet()
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                StartDate = invoice.StartDate,
                EndDate = invoice.EndDate,
                TotalSum = invoice.TotalSum,
                Comment = invoice.Comment,
                CreatedAt = invoice.CreatedAt,
                IsCancelled = invoice.IsCancelled,
                IsCreated = invoice.IsCreated,
                IsPaid = invoice.IsPaid,
                IsReceived = invoice.IsReceived,
                IsRejected = invoice.IsRejected,
                IsSent = invoice.IsSent,
                Rows = resultRows
            };
            return returnResult;
        }

        /// <summary>
        /// Method for download pdf of invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Pdf file</returns>
        public async Task<CustomFile?> DownloadInvoice(int id, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return new CustomFile() { FileName = "unauth"};
            }
            var invoice = await _dbContext.Invoices.FindAsync(id);
            if (invoice is null)
            {
                return null;
            }
            var customer = await _dbContext.Customers.FindAsync(invoice.CustomerId);
            if (customer == null || customer.UserId != user.Id)
            {
                return null;
            }

            List<InvoiceRow> invoiceRows = _dbContext.InvoiceRows.Where(i => i.InvoiceId == invoice.Id).ToList();
            string html = $@"<h1 style=""text-align: center;"">Invoice</h1>
                        <p>Id:{invoice.Id}</p>
                        <p>Customer id : {invoice.CustomerId}</p>
                        <p>Start date : {invoice.StartDate}</p>
                        <p>End date : {invoice.EndDate}</p>
                        <p>Total sum : {invoice.TotalSum}</p>
                        <p>Comment : {invoice.Comment}</p>
                        <h1 style=""text-align: center;"">Rows</h1>
                        <table align=""center"" style=""border: 1px solid; text-align: center; border-collapse: collapse;"">
                            <tr>
                                <th style=""border: 1px solid black;"">Service</th>
                                <th style=""border: 1px solid black;"">Quantity</th>
                                <th style=""border: 1px solid black;"">Amount</th>
                                <th style=""border: 1px solid black;"">Sum</th>
                            </tr>";
            for (int i = 0; i < invoiceRows.Count; i++)
            {
                html += @$"
                        <tr>
                        <td style=""border: 1px solid black;"">{invoiceRows[i].Service}</td>
                        <td style=""border: 1px solid black;"">{invoiceRows[i].Quantity}</td>
                        <td style=""border: 1px solid black;"">{invoiceRows[i].Amount}</td>
                        <td style=""border: 1px solid black;"">{invoiceRows[i].Sum}</td>
                        </tr>";
            }
            html += "\n</table>";
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            PdfDocument document = htmlConverter.Convert(html, "");
            FileStream fileStream = new FileStream("InvoicePdf.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            document.Save(fileStream);
            fileStream.Close();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "InvoicePdf.pdf");
            var stream = new FileStream(path, FileMode.Open);
            return new CustomFile(){fileStream = stream, ContentType = "application/octet-stream"
            , FileName = "InvoicePdf.pdf"};
        }

        /// <summary>
        /// Method for edit invoice
        /// </summary>
        /// <param name="id">Id of the invoice to be updated</param>
        /// <param name="invoice">Invoice details</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Updated invoice</returns>
        public async Task<InvoiceGet?> EditInvoice(int id, InvoiceDto invoice, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return new InvoiceGet();
            }
            var oldInvoice = await _dbContext.Invoices.FindAsync(id);
            if (oldInvoice is null)
            {
                return null;
            }
            var customer = await _dbContext.Customers.FindAsync(oldInvoice.CustomerId);
            if (customer == null || customer.UserId != user.Id)
            {
                return null;
            }
            if (oldInvoice.IsSent)
            {
                return new InvoiceGet() { Id = -1 };
            }
            decimal total = 0;
            foreach (var item in invoice.InvoiceRows)
            {
                total += item.Amount * item.Quantity;
            }
            oldInvoice.CustomerId = customer.Id;
            oldInvoice.StartDate = invoice.StartDate;
            oldInvoice.EndDate = invoice.EndDate;
            oldInvoice.TotalSum = total;
            oldInvoice.Comment = invoice.Comment;
            oldInvoice.UpdatedAt = DateTimeOffset.Now;
            ICollection<InvoiceRow> oldInvoiceRows = _dbContext.InvoiceRows.Where(i => i.InvoiceId == oldInvoice.Id).ToList();
            _dbContext.InvoiceRows.RemoveRange(oldInvoiceRows);
            List<InvoiceRow> resultRows = new List<InvoiceRow>();
            foreach (var item in invoice.InvoiceRows)
            {
                var newInvoiceRow = new InvoiceRow()
                {
                    Id = default,
                    InvoiceId = oldInvoice.Id,
                    Invoice = oldInvoice,
                    Service = item.Service,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    Sum = item.Amount * item.Quantity
                };
                _dbContext.InvoiceRows.Add(newInvoiceRow);
                resultRows.Add(newInvoiceRow);
            }
            await _dbContext.SaveChangesAsync();

            List<InvoiceRowGet> InvoiceResultRows = new();
            foreach (var row in resultRows)
            {
                var item = new InvoiceRowGet()
                {
                    Id = row.Id,
                    Service = row.Service,
                    Sum = row.Sum,
                    Amount = row.Amount,
                    Quantity = row.Quantity,
                };
                InvoiceResultRows.Add(item);
            }
            var returnResult = new InvoiceGet()
            {
                Id = oldInvoice.Id,
                CustomerId = oldInvoice.CustomerId,
                StartDate = oldInvoice.StartDate,
                EndDate = oldInvoice.EndDate,
                TotalSum = oldInvoice.TotalSum,
                Comment = oldInvoice.Comment,
                CreatedAt = oldInvoice.CreatedAt,
                IsCancelled = oldInvoice.IsCancelled,
                IsCreated = oldInvoice.IsCreated,
                IsPaid = oldInvoice.IsPaid,
                IsReceived = oldInvoice.IsReceived,
                IsRejected = oldInvoice.IsRejected,
                IsSent = oldInvoice.IsSent,
                Rows = InvoiceResultRows
            };
            return returnResult;
        }

        /// <summary>
        /// Method for get invoice
        /// </summary>
        /// <param name="id">Id of invoice</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>Invoice</returns>
        public async Task<InvoiceGet?> GetInvoice(int id, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return new InvoiceGet();
            }
            var invoice = await _dbContext.Invoices.FindAsync(id);
            if (invoice is null)
            {
                return null;
            }
            var customer = await _dbContext.Customers.FindAsync(invoice.CustomerId);
            if (customer == null || customer.UserId != user.Id)
            {
                return null;
            }
            ICollection<InvoiceRow> rows = _dbContext.InvoiceRows.Where(ir => ir.InvoiceId == invoice.Id).ToList();
            List<InvoiceRowGet> InvoiceResultRows = new();
            foreach (var row in rows)
            {
                var item = new InvoiceRowGet()
                {
                    Id = row.Id,
                    Service = row.Service,
                    Sum = row.Sum,
                    Amount = row.Amount,
                    Quantity = row.Quantity,
                };
                InvoiceResultRows.Add(item);
            }
            var returnResult = new InvoiceGet()
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                StartDate = invoice.StartDate,
                EndDate = invoice.EndDate,
                TotalSum = invoice.TotalSum,
                Comment = invoice.Comment,
                CreatedAt = invoice.CreatedAt,
                IsCancelled = invoice.IsCancelled,
                IsCreated = invoice.IsCreated,
                IsPaid = invoice.IsPaid,
                IsReceived = invoice.IsReceived,
                IsRejected = invoice.IsRejected,
                IsSent = invoice.IsSent,
                Rows = InvoiceResultRows
            };
            return returnResult;
        }

        /// <summary>
        /// Method for get invoices
        /// </summary>
        /// <param name="page">Count of page</param>
        /// <param name="pageSize">Count of item per page</param>
        /// <param name="search">Search with comment of invoices</param>
        /// <param name="sort">Sort invoices by their total sum</param>
        /// <param name="userId">User's id for authorization</param>
        /// <returns>List of invoices</returns>
        public async Task<PaginatedListDto<InvoiceGet?>?> GetInvoicesList(int page, int pageSize, string? search, SortEnum sort, int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                return null;
            }
            var customers = await _dbContext.Customers.Where(c => c.UserId == user.Id).ToListAsync();
            var invoices = new List<Invoice>();
            foreach (var item in customers)
            {
                invoices.AddRange(_dbContext.Invoices.Where(i => i.CustomerId == item.Id));
            }
            IEnumerable<InvoiceGet> query = new List<InvoiceGet>();
            foreach (var item in invoices)
            {
                ICollection<InvoiceRow> rows = _dbContext.InvoiceRows.Where(ir => ir.InvoiceId == item.Id).ToList();
                List<InvoiceRowGet> InvoiceResultRows = new();
                foreach (var row in rows)
                {
                    var InvoiceRowItem = new InvoiceRowGet()
                    {
                        Id = row.Id,
                        Service = row.Service,
                        Sum = row.Sum,
                        Amount = row.Amount,
                        Quantity = row.Quantity,
                    };
                    InvoiceResultRows.Add(InvoiceRowItem);
                }
                var returnResult = new InvoiceGet()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    TotalSum = item.TotalSum,
                    Comment = item.Comment,
                    CreatedAt = item.CreatedAt,
                    IsCancelled = item.IsCancelled,
                    IsCreated = item.IsCreated,
                    IsPaid = item.IsPaid,
                    IsReceived = item.IsReceived,
                    IsRejected = item.IsRejected,
                    IsSent = item.IsSent,
                    Rows = InvoiceResultRows
                };
                query = query.Append(returnResult);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Comment.Contains(search));
            }
            if (sort == SortEnum.ASC)
            {
                query = query.OrderBy(t => t.TotalSum);
            }
            else if (sort == SortEnum.DESC)
            {
                query = query.OrderByDescending(t => t.TotalSum);
            }
            var totalCount = query.Count();
            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PaginatedListDto<InvoiceGet?>(
                items.Select(t => new InvoiceGet
                {
                    Id = t.Id,
                    CustomerId = t.CustomerId,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    TotalSum = t.TotalSum,
                    Comment = t.Comment,
                    CreatedAt = t.CreatedAt,
                    IsCancelled = t.IsCancelled,
                    IsCreated = t.IsCreated,
                    IsPaid = t.IsPaid,
                    IsReceived = t.IsReceived,
                    IsRejected = t.IsRejected,
                    IsSent = t.IsSent,
                    Rows = t.Rows
                }),
                new PaginationMeta(page, pageSize, totalCount)
                );
        }
    }
}
