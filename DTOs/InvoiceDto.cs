using InvoiceManager.Models;

namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Dto class for add invoice to database
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// Customer's id of invoice
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Start time of invoice
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// End time of invoice
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Comment of invoice
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Rows of invoice
        /// </summary>
        public ICollection<InvoiceRowDto>? InvoiceRows { get; set; }
    }
}
