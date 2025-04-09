using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System;
using Jibit.Domain.Aggregates;

namespace Jibit.API.Validators
{
    public class CardMatchingRequestValidator : AbstractValidator<CardMatchingRequest>
    {
        public CardMatchingRequestValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .Length(16).WithMessage("Card number must be exactly 16 digits long.");

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("National code is required.")
                .Length(10).WithMessage("National code must be exactly 10 digits long.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");
        }
    }
}