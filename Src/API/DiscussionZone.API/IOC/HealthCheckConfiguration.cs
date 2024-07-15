namespace DiscussionZone.API.IOC
{
    public static class HealthCheckConfiguration
    {
        public static void AddHealthCheckConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("PostgreSQL") ?? string.Empty);

            services.AddHealthChecksUI()
                .AddPostgreSqlStorage(configuration.GetConnectionString("PostgreSQL") ?? string.Empty);
        }
    }
}
