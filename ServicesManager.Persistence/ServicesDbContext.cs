using Microsoft.EntityFrameworkCore;
using ServicesManager.Domain.Entities;

namespace ServicesManager.Persistence
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ServiceCategoryEntity> ServicesCategories { get; set; }
    }
}
