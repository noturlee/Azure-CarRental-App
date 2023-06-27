using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace THERIDEURENT1.models;

public partial class TheRideYouRentContext : DbContext
{
    public TheRideYouRentContext()
    {
    }

    public TheRideYouRentContext(DbContextOptions<TheRideYouRentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBodyType> CarBodyTypes { get; set; }

    public virtual DbSet<CarMake> CarMakes { get; set; }

    public virtual DbSet<CarReturn> CarReturns { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Inspector> Inspectors { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:st10033808-l.database.windows.net,1433;Initial Catalog=TheRideYouRent;Persist Security Info=False;User ID=st10033808;Password=Leighche@28;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarNo).HasName("PK__Car__68A00DDDBCF1A7BE");

            entity.ToTable("Car");

            entity.Property(e => e.CarNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Available)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CarBodyTypeId).HasColumnName("CarBodyTypeID");
            entity.Property(e => e.CarMakeId).HasColumnName("CarMakeID");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CarBodyType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarBodyTypeId)
                .HasConstraintName("FK__Car__CarBodyType__27F8EE98");

            entity.HasOne(d => d.CarMake).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarMakeId)
                .HasConstraintName("FK__Car__CarMakeID__2704CA5F");
        });

        modelBuilder.Entity<CarBodyType>(entity =>
        {
            entity.HasKey(e => e.CarBodyTypeId).HasName("PK__CarBodyT__2BA49AEB91CD5C33");

            entity.ToTable("CarBodyType");

            entity.Property(e => e.CarBodyTypeId).HasColumnName("CarBodyTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarMake>(entity =>
        {
            entity.HasKey(e => e.CarMakeId).HasName("PK__CarMake__A125EE7CA87EA526");

            entity.ToTable("CarMake");

            entity.Property(e => e.CarMakeId).HasColumnName("CarMakeID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarReturn>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__CarRetur__F445E9A8111AC15A");

            entity.ToTable("CarReturn");

            entity.Property(e => e.CarNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.ReturnDate).HasColumnType("date");

            entity.HasOne(d => d.CarNoNavigation).WithMany(p => p.CarReturns)
                .HasForeignKey(d => d.CarNo)
                .HasConstraintName("FK__CarReturn__CarNo__13BCEBC1");

            entity.HasOne(d => d.Driver).WithMany(p => p.CarReturns)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__CarReturn__Drive__1699586C");

            entity.HasOne(d => d.InspectorNoNavigation).WithMany(p => p.CarReturns)
                .HasForeignKey(d => d.InspectorNo)
                .HasConstraintName("FK__CarReturn__Inspe__15A53433");

            entity.HasOne(d => d.Rental).WithMany(p => p.CarReturns)
                .HasForeignKey(d => d.RentalId)
                .HasConstraintName("FK__CarReturn__Renta__14B10FFA");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Driver__F1B1CD2420BCE93F");

            entity.ToTable("Driver");

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inspector>(entity =>
        {
            entity.HasKey(e => e.InspectorNo).HasName("PK__Inspecto__5FECAB762B35423F");

            entity.ToTable("Inspector");

            entity.Property(e => e.InspectorNo)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PK__Rental__9700596373F4A0EC");

            entity.ToTable("Rental");

            entity.Property(e => e.RentalId).HasColumnName("RentalID");
            entity.Property(e => e.CarNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.InspectorNo)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.RentalFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.CarNoNavigation).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.CarNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rental__CarNo__0EF836A4");

            entity.HasOne(d => d.Driver).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rental__DriverID__10E07F16");

            entity.HasOne(d => d.InspectorNoNavigation).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.InspectorNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rental__Inspecto__0FEC5ADD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
