using DiscussionZone.Application.DTOs;
using MediatR;

namespace DiscussionZone.Application.Features.Command.Auth.Login
{
    public class LoginCommantRequest : IRequest<BaseResponse<Token>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
