using FluentValidation;
using InvoiceManager.DTOs;

namespace InvoiceManager.Validation
{
    /// <summary>
    /// Validator class for invoice statusses
    /// </summary>
    public class InvoiceStatusValidator : AbstractValidator<InvoiceStatus>
    {
        /// <summary>
        /// Constructor for validation rules
        /// </summary>
        public InvoiceStatusValidator()
        {
            RuleFor(x => x.IsCancelled).NotEmpty().WithMessage("Invoice statusses are required").NotEqual(false)
                .WithMessage("Status must be false or true").NotEqual(true).WithMessage("Status must be false or true");
            RuleFor(x => x.IsPaid).NotEmpty().WithMessage("Invoice statusses are required").NotEqual(false)
                .WithMessage("Status must be false or true").NotEqual(true).WithMessage("Status must be false or true");
            RuleFor(x => x.IsCreated).NotEmpty().WithMessage("Invoice statusses are required").NotEqual(false)
                .WithMessage("Status must be false or true").NotEqual(true).WithMessage("Status must be false or true");
            RuleFor(x => x.IsRejected).NotEmpty().WithMessage("Invoice statusses are required").NotEqual(false)
                .WithMessage("Status must be false or true").NotEqual(true).WithMessage("Status must be false or true");
            RuleFor(x => x.IsReceived).NotEmpty().WithMessage("Invoice statusses are required").NotEqual(false)
                .WithMessage("Status must be false or true").NotEqual(true).WithMessage("Status must be false or true");
            RuleFor(x => x.IsSent).NotEmpty().WithMessage("Invoice statusses are required").NotEqual(false)
                .WithMessage("Status must be false or true").NotEqual(true).WithMessage("Status must be false or true");
        }
    }
}
