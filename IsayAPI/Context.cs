using IsayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Sensor>().HasKey(sensor => sensor.sensor_id);
            modelBuilder.Entity<Sensor>().ToTable("sensors")
                .HasMany(p => p.measurement_types)
                .WithMany(p => p.sensors)
                .UsingEntity<Sensor_Measurement>();

            modelBuilder.Entity<Measurement_Type>().ToTable("measurements_type").HasKey(measurements_type => measurements_type.type_id);
            modelBuilder.Entity<Meteostation_Sensor>().ToTable("meteostations_sensors").HasKey(meteostations_sensors => meteostations_sensors.sensor_id);
            modelBuilder.Entity<Meteostation>().ToTable("meteostations").HasKey(Meteostation => Meteostation.station_id);
        }
        public DbSet<Sensor> Sensors { get; set; } = null;
        public DbSet<Meteostation> Meteostations { get; set; } = null;
        public DbSet<Measurement_Type> Measurements_Type { get; set; } = null;
        public DbSet<Meteostation_Sensor> MeteostationsSensors { get; set; } = null;
    }
}
