namespace InvoiceManager.Models
{
    /// <summary>
    /// Represents the customer in the program
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer's unique id
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
        public string? Email { get; set; }

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
        /// Customer's update time
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Customer's delete time
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Customer's user's unique id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Customer's user
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Customer's invoices
        /// </summary>
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
