using InvoiceManager.Models;

namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Dto class for add customer to database
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Customer's name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Customer's address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Customer's email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Customer's password
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
