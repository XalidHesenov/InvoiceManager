namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Class for reports requests
    /// </summary>
    public class ReportRequest
    {
        /// <summary>
        /// start time of request interval
        /// </summary>
        public DateTime startOfInterval { get; set; }

        /// <summary>
        /// end time of request interval
        /// </summary>
        public DateTime endOfInterval { get; set; }
    }
}
