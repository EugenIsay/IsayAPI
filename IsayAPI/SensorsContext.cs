using IsayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI
{
    public class SensorsContext : DbContext
    {
        public SensorsContext(DbContextOptions<SensorsContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Sensor>().ToTable("sensors").HasKey(sensor => sensor.sensor_id);
        }
        public DbSet<Sensor> Sensors { get; set; } = null;
    }
}
