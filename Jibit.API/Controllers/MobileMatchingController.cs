using Microsoft.AspNetCore.Mvc;
using MediatR;
using Jibit.Application.Commands;
using System.Threading.Tasks;
using Jibit.Domain.Aggregates;

namespace Jibit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MobileMatchingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MobileMatchingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("validate-mobile")]
        public async Task<IActionResult> ValidateMobile([FromBody] MobileMatchingRequest request)
        {
            var command = new ValidateMobileCommand(request.NationalCode, request.MobileNumber);
            var isValid = await _mediator.Send(command);
            if (!isValid)
            {
                return BadRequest("National code and mobile number validation failed.");
            }
            return Ok("National code and mobile number validation succeeded.");
        }
    }
}