using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Filters;
using FluentValidation;

namespace BLL.Validators.Filters
{
    public class ReservationFilterDtoValidator : AbstractValidator<ReservationFilterDto>
    {
        public ReservationFilterDtoValidator()
        {
            RuleFor(x => x.Status)
                .Must(status => string.IsNullOrEmpty(status) || status.ToLower() == "reserved" || status.ToLower() == "returned")
                .WithMessage("Status must be either 'Reserved' or 'Returned' if specified");

            RuleFor(x => x.UserId)
                .GreaterThanOrEqualTo(1)
                .When(x => x.UserId.HasValue)
                .WithMessage("UserId must be greater than or equal to 1 if specified");

            RuleFor(x => x.BookId)
                .GreaterThanOrEqualTo(1)
                .When(x => x.BookId.HasValue)
                .WithMessage("BookId must be greater than or equal to 1 if specified");

            RuleFor(x => x)
                .Must(x => !x.ReservedFrom.HasValue || !x.ReservedTo.HasValue || x.ReservedFrom < x.ReservedTo)
                .WithMessage("ReservedFrom must be less than ReservedTo if both are specified");

            RuleFor(x => x.SortBy)
                .Must(sort => string.IsNullOrEmpty(sort) || sort.ToLower() == "status" || sort.ToLower() == "reservedat" || sort.ToLower() == "id")
                .WithMessage("SortBy must be one of: 'status', 'reservedat', or 'id'");
        }
    }
}
