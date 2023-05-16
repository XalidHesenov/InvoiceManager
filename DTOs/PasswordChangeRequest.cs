namespace InvoiceManager.DTOs
{
    /// <summary>
    /// Class for user's password change informations
    /// </summary>
    public class PasswordChangeRequest
    {
        /// <summary>
        /// User's current password
        /// </summary>
        public string? currentPassword { get; set; }

        /// <summary>
        /// User's new password
        /// </summary>
        public string? NewPassword { get; set; }
    }
}
