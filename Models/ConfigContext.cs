using Microsoft.EntityFrameworkCore;

namespace tech_task.Models
{
    public class ConfigContext : DbContext
    {
        public DbSet<ConfigurationItem> ConfigurationItems { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConfigurationsDatabase;Trusted_Connection=True;");
        }

    }
}