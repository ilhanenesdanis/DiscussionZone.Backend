using DiscussionZone.Application.DTOs;
using MediatR;

namespace DiscussionZone.Application.Features.Command.Category.Create
{
    public class CreateCategoryCommandRequest : IRequest<BaseResponse<CreateCategoryCommandResponse>>
    {
        public string Name { get; set; }
        public Guid? ParentGUID { get; set; }
    }
}
