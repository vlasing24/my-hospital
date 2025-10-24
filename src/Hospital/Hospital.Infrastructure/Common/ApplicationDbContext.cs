using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Common
{
    public sealed class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}