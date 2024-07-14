using DiscussionZone.Application.DTOs;
using DiscussionZone.Application.Exceptions;
using DiscussionZone.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DiscussionZone.Application.Features.Queries.User.GetUserByEmail
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQueryRequest, BaseResponse<GetUserQueryResponse>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<GetUserQueryResponse>> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new InvalidRequestException($"{nameof(GetUserQueryRequest)} is not null");
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new InvalidRequestParameterException($"{nameof(GetUserQueryRequest.Email)} is not empty");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new NotFoundException("User Not Found");

            return new BaseResponse<GetUserQueryResponse>()
            {
                Data = new GetUserQueryResponse()
                {
                    BirthDate = user.BirthDate.HasValue ? user.BirthDate : null,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Job = user.Job,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber,
                }
            };
        }
    }
}
