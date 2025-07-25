using Microsoft.EntityFrameworkCore;
using SensorAPI.Models.Models;

namespace SensorAPI.DataLayer.Context
{
    public class AppDbContext : DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorRecord> SensorRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Sensor>()
                .HasIndex(s => s.name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
