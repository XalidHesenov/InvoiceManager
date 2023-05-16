using InvoiceManager.Models;

namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Class for get result of get invoice methods
    /// </summary>
    public class InvoiceGet
    {
        /// <summary>
        /// Id of invoice
        /// </summary>
        public int Id { get; set; }

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
        /// Total sum of invoice
        /// </summary>
        public Decimal TotalSum { get; set; }

        /// <summary>
        /// Comment of invoice
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Creation time of invoice
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

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

        /// <summary>
        /// Rows of invoice
        /// </summary>
        public ICollection<InvoiceRowGet>? Rows { get; set; }
    }
}
