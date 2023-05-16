
namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Class for get result of get customer methods
    /// </summary>
    public class CustomerGet
    {
        /// <summary>
        /// Customer's id
        /// </summary>
        public int Id { get; set; }

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
        public string?   Email { get; set; }

        /// <summary>
        /// Customer's password
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Customer's creation time
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Customer's updation time
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
