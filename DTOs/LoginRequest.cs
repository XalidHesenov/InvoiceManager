namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Class for user's login informations
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// User's username
        /// </summary>
        public string? Username { get; set; }
        
        /// <summary>
        /// User's password
        /// </summary>
        public string? Password { get; set; }
    }
}
