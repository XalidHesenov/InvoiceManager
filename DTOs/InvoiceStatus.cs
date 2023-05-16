namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Class for invoice status methods
    /// </summary>
    public class InvoiceStatus
    {
        /// <summary>
        /// IsCreated method of invoice
        /// </summary>
        public bool IsCreated { get; set; }

        /// <summary>
        /// IsSent method of invoice
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// IsReceived method of invoice
        /// </summary>
        public bool IsReceived { get; set; }

        /// <summary>
        /// IsPaid method of invoice
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// IsCancelled method of invoice
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// IsRejected method of invoice
        /// </summary>
        public bool IsRejected { get; set; }
    }
}
