using IsayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI
{
    public class MSContext : DbContext
    {
        public MSContext(DbContextOptions<MSContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<meteostations_sensors>().ToTable("meteostations_sensors").HasKey(meteostations_sensors => meteostations_sensors.sensor_id);
        }
        public DbSet<meteostations_sensors> MeteostationsSensors { get; set; } = null;
    }
}