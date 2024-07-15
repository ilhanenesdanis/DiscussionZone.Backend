using DiscussionZone.Application.DTOs;
using DiscussionZone.Domain;

namespace DiscussionZone.Application.Abstract
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}
