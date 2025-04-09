using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Jibit.Domain.Aggregates;

namespace Jibit.Application.Validators
{
    public class MobileMatchingRequestValidator : AbstractValidator<MobileMatchingRequest>
    {
        public MobileMatchingRequestValidator()
        {
            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("NationalCode is required.")
                .Length(10).WithMessage("NationalCode must be exactly 10 digits long.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("MobileNumber is required.")
                .Matches(@"^09\d{9}$").WithMessage("Mobile number must be in the format '09xxxxxxxxx'.");

        }
    }
}