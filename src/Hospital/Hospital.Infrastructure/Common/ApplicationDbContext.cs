using Hospital.Domain.Appointment;
using Hospital.Domain.Patient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hospital.Infrastructure.Common;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Doctor> Doctor => Set<Doctor>();

    private readonly string _connectionString;

    public ApplicationDbContext(IOptions<PostgresConnectionOptions> options)
    {
        _connectionString = options.Value.AsConnectionString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}