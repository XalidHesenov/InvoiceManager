namespace InvoiceManager.Models
{
    /// <summary>
    /// Represents the user in the program
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's unique id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's unique username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// User's address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// User's creation time
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// User's update time
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// User's customers
        /// </summary>
        ICollection<Customer>? Customers { get; set; }
    }
}
