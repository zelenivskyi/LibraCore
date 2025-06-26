using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Reservation;
using FluentValidation;

namespace BLL.Validators.Reservation
{
    public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateDto>
    {
        public ReservationCreateDtoValidator() 
        {
            RuleFor(p => p.UserId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("User ID cannot be less than 1");

            RuleFor(p => p.BookId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Book ID cannot be less than 1");
        }
    }
}
