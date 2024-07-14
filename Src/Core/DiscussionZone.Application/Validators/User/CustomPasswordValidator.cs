using DiscussionZone.Domain;
using Microsoft.AspNetCore.Identity;

namespace DiscussionZone.Application.Validators.User
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            List<IdentityError> err = new List<IdentityError>();
            if (string.IsNullOrWhiteSpace(password))
            {
                err.Add(new IdentityError() { Code = "emptypassword", Description = "Password bilgisi boş olmamalıdır" });
                return await Task.FromResult(IdentityResult.Failed(err.ToArray()));
            }

            if (password.Length < 6)
                err.Add(new IdentityError() { Code = "passwordlength", Description = "Password minimum 6 karakter olmalıdır" });
            if (password.ToLower().Equals(user.UserName.ToLower()))
                err.Add(new IdentityError() { Code = "PasswordContainsUserName", Description = "Password username ile aynı olmamalıdır" });
            if (password.Contains(user.FirstName))
                err.Add(new IdentityError() { Code = "passwordcontainsFirstName", Description = "Password isim içermemelidir" });
            if (password.Contains(user.LastName))
                err.Add(new IdentityError() { Code = "passwordcontainsLastName", Description = "Password soyisim içermemelidir" });

            if (err.Any())
                return await Task.FromResult(IdentityResult.Failed(err.ToArray()));
            return await Task.FromResult(IdentityResult.Success);
        }
    }
}
