using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DiscussionZone.Application.Extensions
{
    public static class DependencyExtension
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
