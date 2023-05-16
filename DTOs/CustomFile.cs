namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Custom file class for convert invoice to pdf
    /// </summary>
    public class CustomFile
    {
        /// <summary>
        /// File stream
        /// </summary>
        public Stream? fileStream { get; set; }

        /// <summary>
        /// Type of file
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// Name of file
        /// </summary>
        public string? FileName { get; set; }
    }
}
