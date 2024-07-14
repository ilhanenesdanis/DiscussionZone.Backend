using DiscussionZone.Application.Features.Command.User.Create;
using DiscussionZone.Application.Features.Queries.User.GetUserByEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionZone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string email)
        {
            return Ok(await _mediator.Send(new GetUserQueryRequest() { Email = email }));
        }
    }
}
