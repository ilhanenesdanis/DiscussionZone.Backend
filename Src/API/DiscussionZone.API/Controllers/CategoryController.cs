using DiscussionZone.Application.Features.Command.Category.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionZone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
