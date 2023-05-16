using InvoiceManager.Models;

namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Dto class for add rows of invoice to database
    /// </summary>
    public class InvoiceRowDto
    {
        /// <summary>
        /// Name of work
        /// </summary>
        public string? Service { get; set; }

        /// <summary>
        /// Quantity of work
        /// </summary>
        public Decimal Quantity { get; set; }

        /// <summary>
        /// amount of work
        /// </summary>
        public Decimal Amount { get; set; }
    }
}
