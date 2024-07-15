using DiscussionZone.Application.DTOs;

namespace DiscussionZone.Application.Services
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string email, string password, int accessTokenLifeTime);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
