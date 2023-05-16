namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Dto class for add user to database
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// User's username
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// User's address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
