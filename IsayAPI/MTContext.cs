using IsayAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI
{
    public class MTContext : DbContext
    {
        public MTContext(DbContextOptions<MTContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Measurement_Type>().ToTable("measurements_type").HasKey(measurements_type => measurements_type.type_id);
        }
        public DbSet<Measurement_Type> Measurements_Type { get; set; } = null;
    }
}