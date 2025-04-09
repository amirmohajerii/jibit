using Jibit.Application.Interfaces;
using MediatR;

namespace Jibit.Application.Services.Commands
{
    public class ValidateCardCommand : IRequest<bool>
    {
        public string CardNumber { get; }
        public string NationalCode { get; }
        public DateTime BirthDate { get; }

        public ValidateCardCommand(string cardNumber, string nationalCode, DateTime birthDate)
        {
            CardNumber = cardNumber;
            NationalCode = nationalCode;
            BirthDate = birthDate;
        }
    }

    public class ValidateCardCommandHandler : IRequestHandler<ValidateCardCommand, bool>
    {
        private readonly ICardMatchingService _cardMatchingService;

        public ValidateCardCommandHandler(ICardMatchingService cardMatchingService)
        {
            _cardMatchingService = cardMatchingService;
        }

        public async Task<bool> Handle(ValidateCardCommand command, CancellationToken cancellationToken)
        {
            return await _cardMatchingService.MatchCardWithNationalCodeAsync(
                command.CardNumber,
                command.NationalCode,
                command.BirthDate
            );
        }
    }


}