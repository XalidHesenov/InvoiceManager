namespace InvoiceManager.Models
{
    /// <summary>
    /// Represents the rows of invoice in the program
    /// </summary>
    public class InvoiceRow
    {
        /// <summary>
        /// Unique id of the rows of invoice
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Unique id of the invoice of the rows
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Invoice of the rows
        /// </summary>
        public Invoice? Invoice { get; set; }

        /// <summary>
        /// Name of work
        /// </summary>
        public string? Service { get; set; }

        /// <summary>
        /// Quantity of work
        /// </summary>
        public Decimal Quantity { get; set; }

        /// <summary>
        /// Amount of Work
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Total sum of work
        /// </summary>
        public Decimal Sum { get; set; }
    }
}
