using Jibit.Application.Interfaces;
using MediatR;

namespace Jibit.Application.Commands
{
    public class ValidateMobileCommand : IRequest<bool>
    {
        public string NationalCode { get; }
        public string MobileNumber { get; }

        public ValidateMobileCommand(string nationalCode, string mobileNumber)
        {
            NationalCode = nationalCode;
            MobileNumber = mobileNumber;
        }
    }

    public class ValidateMobileCommandHandler : IRequestHandler<ValidateMobileCommand, bool>
    {
        private readonly IMobileMatchingService _mobileMatchingService;

        public ValidateMobileCommandHandler(IMobileMatchingService mobileMatchingService)
        {
            _mobileMatchingService = mobileMatchingService;
        }

        public async Task<bool> Handle(ValidateMobileCommand command, CancellationToken cancellationToken)
        {
            return await _mobileMatchingService.MatchNationalCodeWithMobileAsync(
                command.NationalCode,
                command.MobileNumber
            );
        }
    }


}