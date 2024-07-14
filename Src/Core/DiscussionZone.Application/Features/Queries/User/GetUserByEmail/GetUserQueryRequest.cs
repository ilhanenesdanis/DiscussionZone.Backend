using DiscussionZone.Application.DTOs;
using MediatR;

namespace DiscussionZone.Application.Features.Queries.User.GetUserByEmail
{
    public class GetUserQueryRequest : IRequest<BaseResponse<GetUserQueryResponse>>
    {
        public required string Email { get; set; }
    }
}
