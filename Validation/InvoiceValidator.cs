using FluentValidation;
using InvoiceManager.DTOs;

namespace InvoiceManager.Validation
{
    /// <summary>
    /// Validator class for invoices
    /// </summary>
    public class InvoiceValidator : AbstractValidator<InvoiceDto>
    {
        /// <summary>
        /// Constructor for validation rules
        /// </summary>
        public InvoiceValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is reuired.");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Start date is reuired.");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is reuired.").Length(5, 40).WithMessage("The length of the comment must be a minimum of 5 and a maximum of 40.");
            RuleFor(x => x.InvoiceRows).NotNull().NotEmpty().WithMessage("Rows cannot be empty");
            RuleForEach(x => x.InvoiceRows)
                .ChildRules(rows =>
                {
                    rows.RuleFor(row => row.Service)
                       .NotEmpty().WithMessage("Service cannot be empty.")
                       .Length(2, 50).WithMessage("Seevice must be between 2 and 50 characters.")
                       .Matches("^[a-zA-Z0-9]+$").WithMessage("Seevice can only consist of letters and digits.");
                    rows.RuleFor(row => row.Amount).NotEmpty().WithMessage("Amount cannot be empty.").GreaterThan(0).WithMessage("Amount must be a positive number.");
                    rows.RuleFor(row => row.Quantity).NotEmpty().WithMessage("Quantity cannot be empty.").GreaterThan(0).WithMessage("Quantity must be a positive number.");
                });
        }
    }
}