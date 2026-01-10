using Hospital.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static void AddPostgres(this IServiceCollection services)
        {
            services.AddOptions<PostgresConnectionOptions>().BindConfiguration("PostgresConnectionOptions");

            services.AddScoped<ApplicationDbContext>();
        }
    }
}