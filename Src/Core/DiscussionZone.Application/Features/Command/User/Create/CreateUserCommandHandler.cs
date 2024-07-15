using DiscussionZone.Application.Constants;
using DiscussionZone.Application.DTOs;
using DiscussionZone.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DiscussionZone.Application.Features.Command.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, BaseResponse<bool>>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<bool>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(new AppUser
            {
                Id=Guid.NewGuid().ToString(),
                Email = request.Email,
                BirthDate = request.BirthDate,
                Job = request.Job,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                PhoneNumber = request.Phone,
                EmailConfirmed=true,
                PhoneNumberConfirmed=true,
            }, request.Password);

            if (result.Succeeded)
                return BaseResponse<bool>.Success(result.Succeeded, ResponseMessage.SuccessMessage);
            return BaseResponse<bool>.Error(result.Errors.Select(x => x.Description).FirstOrDefault() ?? string.Empty);

        }
    }
}
