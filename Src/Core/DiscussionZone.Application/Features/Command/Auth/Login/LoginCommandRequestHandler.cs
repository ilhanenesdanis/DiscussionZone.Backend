using DiscussionZone.Application.Constants;
using DiscussionZone.Application.DTOs;
using DiscussionZone.Application.Services;
using MediatR;

namespace DiscussionZone.Application.Features.Command.Auth.Login
{
    public class LoginCommandRequestHandler : IRequestHandler<LoginCommantRequest, BaseResponse<Token>>
    {
        private readonly IAuthService _authService;

        public LoginCommandRequestHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<BaseResponse<Token>> Handle(LoginCommantRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password, 60);
            if (result != null)
                return new BaseResponse<Token>()
                {
                    Message = ResponseMessage.SuccessMessage,
                    Data = result,
                };

            return new BaseResponse<Token>()
            {
                Message = ResponseMessage.FailMessage
            };
        }
    }
}
