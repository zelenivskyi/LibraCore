using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Reservation;
using FluentValidation;

namespace BLL.Validators.Reservation
{
    public class ReservationUpdateDtoValidator : AbstractValidator<ReservationUpdateDto>
    {
        public ReservationUpdateDtoValidator()
        {
            RuleFor(p => p.ReturnedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Returned date cannot be in the future!");
        }
    }
}
