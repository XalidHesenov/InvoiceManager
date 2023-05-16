using FluentValidation;
using InvoiceManager.DTOs;

namespace InvoiceManager.Validation
{
    /// <summary>
    /// Validator class for password change requests
    /// </summary>
    public class PasswordChangeRequestValidator : AbstractValidator<PasswordChangeRequest>
    {
        /// <summary>
        /// Constructor for validation rules
        /// </summary>
        public PasswordChangeRequestValidator()
        {
            RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).*$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }
    }
}
