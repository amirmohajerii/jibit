using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Jibit.Application.Validators
{
    public class IbanValidator : AbstractValidator<string>
    {
        public IbanValidator()
        {
            RuleFor(iban => iban)
                .NotEmpty().WithMessage("IBAN is required.")
                .Length(26).WithMessage("IBAN must be exactly 26 characters.")
                .Must(iban => iban.StartsWith("IR")).WithMessage("IBAN must start with 'IR'.");
        }
    }
}