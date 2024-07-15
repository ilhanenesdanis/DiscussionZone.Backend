using DiscussionZone.Application.Abstract;
using DiscussionZone.Application.DTOs;
using DiscussionZone.Application.Exceptions;
using DiscussionZone.Application.Services;
using DiscussionZone.Domain;
using Microsoft.AspNetCore.Identity;

namespace DiscussionZone.Persistence.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> LoginAsync(string email, string password, int accessTokenLifeTime)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException("Kullanıcı Bulunamadı");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
            if(result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(60, user);
                return token;
            }
            throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya parola");

        }

        public Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
