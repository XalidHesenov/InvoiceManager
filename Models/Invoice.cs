namespace InvoiceManager.Models
{
    /// <summary>
    /// Represents the customer in the program
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Unique id of the invoice
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customer's id of invoice
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The customer of invoice
        /// </summary>
        private Customer? Customer { get; set; }

        /// <summary>
        /// Start time of invoice
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// End time of invoice
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// The rows of invoice
        /// </summary>
        private ICollection<InvoiceRow>? Rows { get; set; }

        /// <summary>
        /// Total sum of invoice
        /// </summary>
        public Decimal TotalSum { get; set; }

        /// <summary>
        /// Comment about invoice
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Creation time of invoice
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Update time of invoice
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Delete time of invoice
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// one of the states of the invoice
        /// </summary>
        public bool IsCreated { get; set; }

        /// <summary>
        /// one of the states of the invoice
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// one of the states of the invoice
        /// </summary>
        public bool IsReceived { get; set; }

        /// <summary>
        /// one of the states of the invoice
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// one of the states of the invoice
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// one of the states of the invoice
        /// </summary>
        public bool IsRejected { get; set; }
    }
}
