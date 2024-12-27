using MediManager_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Database;

public class MediManagerDbContext : DbContext
{
    public MediManagerDbContext() { }
    public MediManagerDbContext(DbContextOptions<MediManagerDbContext> options) : base(options)
    {
    }

    public DbSet<QuantityUnit> QuantityUnits { get; set; }
    public DbSet<MedicationGroup> MedicationGroups { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseMedicationGroup> WarehouseMedicationGroups { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }
    public DbSet<MovementReason> MovementReasons { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }
    public DbSet<ExportLog> ExportLogs { get; set; }
    public DbSet<AuthenticationLog> AuthenticationLogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSetting> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // QuantityUnit-Konfiguration
        modelBuilder.Entity<QuantityUnit>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        // MedicationGroup-Konfiguration
        modelBuilder.Entity<MedicationGroup>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.SupplyNumber).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.HasOne(e => e.QuantityUnit)
                    .WithMany()
                    .HasForeignKey(e => e.QuantityUnitID);
        });

        // Medication-Konfiguration
        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.PZN).IsRequired();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Manufacturer).HasMaxLength(255);
            entity.Property(e => e.SAPNumber).HasMaxLength(255);
            entity.Property(e => e.SAPName).HasMaxLength(255);
            entity.HasOne(e => e.MedicationGroup)
                    .WithMany()
                    .HasForeignKey(e => e.MedicationGroupID);
        });

        // Warehouse-Konfiguration
        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
        });

        // WarehouseMedicationGroup-Konfiguration
        modelBuilder.Entity<WarehouseMedicationGroup>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.HasOne(e => e.Warehouse)
                    .WithMany()
                    .HasForeignKey(e => e.WarehouseID);
            entity.HasOne(e => e.MedicationGroup)
                    .WithMany()
                    .HasForeignKey(e => e.MedicationGroupID);
        });

        // Shelf-Konfiguration
        modelBuilder.Entity<Shelf>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.HasOne(e => e.Warehouse)
                    .WithMany()
                    .HasForeignKey(e => e.WarehouseID);
        });

        // Stock-Konfiguration
        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Batch).HasMaxLength(255);
            entity.Property(e => e.SerialNumber).HasMaxLength(255);
            entity.Property(e => e.ExpireDate).HasMaxLength(255);
            entity.HasOne(e => e.Warehouse)
                    .WithMany()
                    .HasForeignKey(e => e.WarehouseID);
            entity.HasOne(e => e.MedicationGroup)
                    .WithMany()
                    .HasForeignKey(e => e.MedicationGroupID);
            entity.HasOne(e => e.Shelf)
                    .WithMany()
                    .HasForeignKey(e => e.ShelfID);
            entity.HasOne(e => e.Medication)
                    .WithMany()
                    .HasForeignKey(e => e.MedicationID);
        });

        // MovementReason-Konfiguration
        modelBuilder.Entity<MovementReason>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.ReasonCode).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.MovementType).IsRequired().HasMaxLength(255);
        });

        // StockMovement-Konfiguration
        modelBuilder.Entity<StockMovement>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Batch).HasMaxLength(255);
            entity.Property(e => e.SerialNumber).HasMaxLength(255);
            entity.Property(e => e.ExpireDate).HasMaxLength(255);
            entity.Property(e => e.Timestamp).HasMaxLength(255);
            entity.HasOne(e => e.MovementReason)
                    .WithMany()
                    .HasForeignKey(e => e.MovementReasonID);
        });

        // Log-Konfigurationen
        ConfigureLogEntities(modelBuilder);

        // User-Konfigurationen
        ConfigureUserEntities(modelBuilder);
    }

    private void ConfigureLogEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Action).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Timestamp).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<ExportLog>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Timestamp).HasMaxLength(255);
            entity.Property(e => e.ExportType).HasMaxLength(255);
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FileFormat).HasMaxLength(255);
        });

        modelBuilder.Entity<AuthenticationLog>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.Timestamp).HasMaxLength(255);
            entity.Property(e => e.IP).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });
    }

    private void ConfigureUserEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(255);
        });

        modelBuilder.Entity<UserSetting>(entity =>
        {
            entity.HasKey(e => e.ID);
            entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<UserSetting>(e => e.UserID);
        });

        // Seed für MovementReasons
        modelBuilder.Entity<MovementReason>().HasData(
            new MovementReason
            {
                ID = 1,
                ReasonCode = "PATIENT_HANDOUT",
                Description = "Abgabe an Patient",
                MovementType = "OUT"
            },
            new MovementReason
            {
                ID = 2,
                ReasonCode = "DESTRUCTION",
                Description = "Vernichtung",
                MovementType = "OUT"
            },
            new MovementReason
            {
                ID = 3,
                ReasonCode = "EXPIRED",
                Description = "Verfallen",
                MovementType = "OUT"
            },
            new MovementReason
            {
                ID = 4,
                ReasonCode = "WAREHOUSE_TRANSFER_OUT",
                Description = "Umlagerung (Ausgang)",
                MovementType = "OUT"
            },
            new MovementReason
            {
                ID = 5,
                ReasonCode = "WAREHOUSE_TRANSFER_IN",
                Description = "Umlagerung (Eingang)",
                MovementType = "IN"
            },
            new MovementReason
            {
                ID = 6,
                ReasonCode = "NEW_STOCK",
                Description = "Neue Lieferung",
                MovementType = "IN"
            },
            new MovementReason
            {
                ID = 7,
                ReasonCode = "CORRECTION_IN",
                Description = "Korrektur (Zugang)",
                MovementType = "IN"
            },
            new MovementReason
            {
                ID = 8,
                ReasonCode = "CORRECTION_OUT",
                Description = "Korrektur (Abgang)",
                MovementType = "OUT"
            }
        );

        // Seed für QuantityUnits
        modelBuilder.Entity<QuantityUnit>().HasData(
            new QuantityUnit
            {
                ID = 1,
                Name = "Stück",
                Description = "Einzelne Einheit (EA)"
            },
            new QuantityUnit
            {
                ID = 2,
                Name = "Flasche",
                Description = "Flüssigkeiten in Flasche (BT)"
            },
            new QuantityUnit
            {
                ID = 3,
                Name = "Milliliter",
                Description = "Flüssigkeitsmenge in ml"
            },
            new QuantityUnit
            {
                ID = 4,
                Name = "Gramm",
                Description = "Gewicht in g"
            }
        );
    }
}
