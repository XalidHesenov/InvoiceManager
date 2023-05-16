using FluentValidation;
using InvoiceManager.DTOs;

namespace InvoiceManager.Validation
{
    /// <summary>
    /// Validator class for users
    /// </summary>
    public class ReportsValidator : AbstractValidator<ReportRequest>
    {
        /// <summary>
        /// Constructor for validation rules
        /// </summary>
        public ReportsValidator()
        {
            RuleFor(x => x.startOfInterval).NotEmpty().WithMessage("Start date is reuired.");
            RuleFor(x => x.endOfInterval).NotEmpty().WithMessage("Start date is reuired.");
        }
    }
}
