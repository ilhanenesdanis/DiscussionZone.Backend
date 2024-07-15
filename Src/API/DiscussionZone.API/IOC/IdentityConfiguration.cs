using DiscussionZone.Application.Validators.User;
using DiscussionZone.Domain;
using DiscussionZone.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace DiscussionZone.API.IOC
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(conf =>
            {
                conf.Password.RequiredLength = 6;
                conf.Password.RequireNonAlphanumeric = true;
                conf.Password.RequireLowercase = true;
                conf.Password.RequireUppercase = true;
                conf.Password.RequireDigit = true;
                conf.User.RequireUniqueEmail = true;


                conf.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                conf.Lockout.MaxFailedAccessAttempts = 5;
                conf.Lockout.AllowedForNewUsers = true;

                conf.SignIn.RequireConfirmedEmail = true;
                conf.SignIn.RequireConfirmedPhoneNumber = false;

            }).AddEntityFrameworkStores<AppDbContext>()
            .AddPasswordValidator<CustomPasswordValidator>()
            .AddDefaultTokenProviders();
        }
    }
}
