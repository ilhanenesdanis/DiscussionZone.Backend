using DiscussionZone.Application.Repository;
using DiscussionZone.Application.UnitOfWork;
using DiscussionZone.Persistence.Context;
using DiscussionZone.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionZone.Persistence.IOC
{
    public static class DependencyExtension
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();


            services.AddScoped<ICategoryRepository, CategoryRepository>();

        }
    }
}
