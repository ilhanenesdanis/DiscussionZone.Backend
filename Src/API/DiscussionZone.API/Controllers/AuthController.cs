using DiscussionZone.Application.Features.Command.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionZone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommantRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
