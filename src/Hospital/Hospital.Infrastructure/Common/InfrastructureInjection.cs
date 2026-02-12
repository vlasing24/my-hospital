using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Common;
using Hospital.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infrastructure;

public static class InfrastructureInjection
{
    public static void AddPostgres(this IServiceCollection services)
    {
        services.AddOptions<PostgresConnectionOptions>()
            .BindConfiguration("PostgresConnectionOptions");

        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPatientsRepository, PatientsRepository>();
        services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
        services.AddScoped<IDoctorsRepository, DoctorsRepository>();
        services.AddScoped<IPendingEmailsRepository, PendingEmailsRepository>();
        services.AddScoped<ITransactionFactory, TransactionFactory>();
    }
}