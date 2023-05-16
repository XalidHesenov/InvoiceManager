using InvoiceManager.Models;

namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Class for get result of get invoice methods
    /// </summary>
    public class InvoiceRowGet
    {
        /// <summary>
        /// Id of row
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of work
        /// </summary>
        public string? Service { get; set; }

        /// <summary>
        /// Quantity of work
        /// </summary>
        public Decimal Quantity { get; set; }

        /// <summary>
        /// Amount of work
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Total sum of work
        /// </summary>
        public Decimal Sum { get; set; }
    }
}
