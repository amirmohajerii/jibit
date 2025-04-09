using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Jibit.Application.Services.Commands;

namespace Jibit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IbanInquiryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IbanInquiryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetIbanInfo([FromBody] IbanRequest request)
        {
            try
            {
                var command = new GetIbanInfoCommand(request.Iban);
                var ibanInfo = await _mediator.Send(command);
                return Ok(ibanInfo);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}