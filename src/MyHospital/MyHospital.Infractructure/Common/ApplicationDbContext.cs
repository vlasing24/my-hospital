using Microsoft.EntityFrameworkCore;


namespace MyHospital.Infrastructure.Common
{
    public sealed class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}