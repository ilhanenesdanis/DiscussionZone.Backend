using DiscussionZone.Application.Abstract;
using DiscussionZone.Infrasracture.Token;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionZone.Infrasracture.IOC
{
    public static class DependencyExtension
    {
        public static void AddInfrasractureService(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
