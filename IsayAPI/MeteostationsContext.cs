using IsayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI
{
    public class MeteostationsContext : DbContext
    {
        public MeteostationsContext(DbContextOptions<MeteostationsContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Meteostation>().ToTable("meteostations").HasKey(Meteostation => Meteostation.station_id);
        }
        public DbSet<Meteostation> Meteostations { get; set; } = null;
    }
}