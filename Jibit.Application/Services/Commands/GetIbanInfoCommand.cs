using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jibit.Application.Interfaces;
using Jibit.Infra.Services;
using MediatR;

namespace Jibit.Application.Services.Commands
{
    public class GetIbanInfoCommand : IRequest<IbanResponse>
    {
        public string Iban { get; }

        public GetIbanInfoCommand(string iban)
        {
            Iban = iban;
        }
    }
    public class GetIbanInfoCommandHandler : IRequestHandler<GetIbanInfoCommand, IbanResponse>
    {
        private readonly IIbanService _ibanService;

        public GetIbanInfoCommandHandler(IIbanService ibanService)
        {
            _ibanService = ibanService;
        }

        public async Task<IbanResponse> Handle(GetIbanInfoCommand command, CancellationToken cancellationToken)
        {
            return await _ibanService.GetIbanInfoAsync(command.Iban);
        }
    }

}