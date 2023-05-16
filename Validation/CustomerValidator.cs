using FluentValidation;
using InvoiceManager.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace InvoiceManager.Validation
{
    /// <summary>
    /// Validator class for customers
    /// </summary>
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        /// <summary>
        /// Constructor for validation rules
        /// </summary>
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty").Length(2, 20).WithMessage("The length of the name must be a minimum of 2 and a maximum of 20.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address cannot be empty").Length(2, 40).WithMessage("The length of the address must be a minimum of 2 and a maximum of 40.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("enter the email correctly");
            RuleFor(x => x.PhoneNumber).Matches(@"^[+]?[0-9]*$").WithMessage("Phone number can only contain digits or digits and a plus sign (+).");
            RuleFor(x => x.PhoneNumber).Matches(@"^[+]?[0-9].*$").WithMessage("The plus sign (+) must come before the digits.");
            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).*$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }
    }
}
