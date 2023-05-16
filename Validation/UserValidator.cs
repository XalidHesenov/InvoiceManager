using FluentValidation;
using InvoiceManager.DTOs;

namespace InvoiceManager.Validation
{
    /// <summary>
    /// Validator class for users
    /// </summary>
    public class UserValidator : AbstractValidator<UserDto>
    {
        /// <summary>
        /// Constructor for validation rules
        /// </summary>
        public UserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.").Length(5, 15).WithMessage("Username must be between 3 and 20 characters.")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.");
            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).*$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address cannot be empty").Length(2, 40).WithMessage("The length of the address must be a minimum of 2 and a maximum of 40.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required.").EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.PhoneNumber).Matches(@"^[+]?[0-9]*$").WithMessage("Phone number can only contain digits or digits and a plus sign (+).");
            RuleFor(x => x.PhoneNumber).Matches(@"^[+]?[0-9].*$").WithMessage("The plus sign (+) must come before the digits.");
        }
    }
}
