using Microsoft.AspNetCore.Mvc;
using MediatR;
using Jibit.Application.Services.Commands;
using System.Threading.Tasks;
using Jibit.Domain.Aggregates;

namespace Jibit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardMatchingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardMatchingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("validate-card")]
        public async Task<IActionResult> ValidateCard([FromBody] CardMatchingRequest request)
        {
            var command = new ValidateCardCommand(request.CardNumber, request.NationalCode, request.BirthDate);
            var isValid = await _mediator.Send(command);
            if (!isValid)
            {
                return BadRequest("Card and national code validation failed.");
            }
            return Ok("Card and national code validation succeeded.");
        }
    }
}