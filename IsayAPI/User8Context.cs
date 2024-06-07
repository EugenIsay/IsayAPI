using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using IsayAPI.Models;

namespace IsayAPI;

public partial class User8Context : DbContext
{
    public User8Context()
    {
    }

    public User8Context(DbContextOptions<User8Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<MeasurementsType> MeasurementsTypes { get; set; }

    public virtual DbSet<Meteostation> Meteostations { get; set; }

    public virtual DbSet<MeteostationsSensor> MeteostationsSensors { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<SensorsMeasurement> SensorsMeasurements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=193.176.78.35;Port=5433;Username=user8;Password=xKdhr");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("measurements");

            entity.Property(e => e.MeasurementTs)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("measurement_ts");
            entity.Property(e => e.MeasurementType).HasColumnName("measurement_type");
            entity.Property(e => e.MeasurementValue)
                .HasPrecision(17, 2)
                .HasColumnName("measurement_value");
            entity.Property(e => e.SensorInventoryNumber).HasColumnName("sensor_inventory_number");

            entity.HasOne(d => d.MeasurementTypeNavigation).WithMany()
                .HasForeignKey(d => d.MeasurementType)
                .HasConstraintName("measurements_measurements_type_fk");

            entity.HasOne(d => d.SensorInventoryNumberNavigation).WithMany()
                .HasForeignKey(d => d.SensorInventoryNumber)
                .HasConstraintName("measurements_meteostations_sensors_fk");
        });

        modelBuilder.Entity<MeasurementsType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("measurements_type_id");

            entity.ToTable("measurements_type");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(31)
                .HasColumnName("type_name");
            entity.Property(e => e.TypeUnits)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("type_units");
        });

        modelBuilder.Entity<Meteostation>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("meteostations_id");

            entity.ToTable("meteostations");

            entity.Property(e => e.StationId)
                .ValueGeneratedNever()
                .HasColumnName("station_id");
            entity.Property(e => e.StationLatitude)
                .HasPrecision(5, 2)
                .HasColumnName("station_latitude");
            entity.Property(e => e.StationLongitude)
                .HasPrecision(5, 2)
                .HasColumnName("station_longitude");
            entity.Property(e => e.StationName)
                .HasMaxLength(127)
                .HasColumnName("station_name");
        });

        modelBuilder.Entity<MeteostationsSensor>(entity =>
        {
            entity.HasKey(e => e.SensorInventoryNumber).HasName("meteostations_sensors_id");

            entity.ToTable("meteostations_sensors");

            entity.Property(e => e.SensorInventoryNumber)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sensor_inventory_number");
            entity.Property(e => e.AddedTs)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added_ts");
            entity.Property(e => e.RemovedTs)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("removed_ts");
            entity.Property(e => e.SensorId).HasColumnName("sensor_id");
            entity.Property(e => e.StationId).HasColumnName("station_id");

            entity.HasOne(d => d.Sensor);

            entity.HasOne(d => d.Station).WithMany(p => p.MeteostationsSensors)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("meteostations_sensors_meteostations_fk");
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.SensorId).HasName("sensors_id");

            entity.ToTable("sensors");

            entity.Property(e => e.SensorId)
                .ValueGeneratedNever()
                .HasColumnName("sensor_id");
            entity.Property(e => e.SensorName)
                .HasMaxLength(31)
                .HasColumnName("sensor_name");
        });

        modelBuilder.Entity<SensorsMeasurement>(entity =>
        {
            entity
                .HasKey(e => new { e.SensorId, e.TypeId });
            entity.ToTable("sensors_measurements");
            entity.Property(e => e.MeasurementFormula)
                .HasMaxLength(255)
                .HasColumnName("measurement_formula");
            entity.Property(e => e.SensorId).HasColumnName("sensor_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Sensor).WithMany()
                .HasForeignKey(d => d.SensorId)
                .HasConstraintName("sensors_measurements_sensors_fk");

            entity.HasOne(d => d.Type).WithMany()
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("sensors_measurements_measurements_type_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
