using DiscussionZone.Application.DTOs;
using MediatR;

namespace DiscussionZone.Application.Features.Command.User.Create
{
    public class CreateUserCommandRequest : IRequest<BaseResponse<bool>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Job { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
    }
}
