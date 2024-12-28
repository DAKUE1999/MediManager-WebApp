using MediManager_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MediManager_WebApp.Database
{
    public class MediManagerDbContext : DbContext
    {
        public MediManagerDbContext(DbContextOptions<MediManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuantityUnit> QuantityUnits { get; set; }
        public DbSet<MedicationGroup> MedicationGroups { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<WarehouseMedicationGroup> WarehouseMedicationGroups { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<MovementReason> MovementReasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MedicationGroup -> QuantityUnit
            modelBuilder.Entity<MedicationGroup>()
                .HasOne(mg => mg.QuantityUnit)
                .WithMany()
                .HasForeignKey(mg => mg.QuantityUnitID);

            // Medication -> MedicationGroup
            modelBuilder.Entity<Medication>()
                .HasOne(m => m.MedicationGroup)
                .WithMany()
                .HasForeignKey(m => m.MedicationGroupID);

            // Shelf -> Warehouse
            modelBuilder.Entity<Shelf>()
                .HasOne(s => s.Warehouse)
                .WithMany(w => w.Shelves)
                .HasForeignKey(s => s.WarehouseID);

            modelBuilder.Entity<WarehouseMedicationGroup>()
                .HasOne(s => s.Warehouse)
                .WithMany(w => w.WarehouseMedicationGroups)
                .HasForeignKey(s => s.WarehouseID);

            // Stock Beziehungen
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasOne(s => s.Warehouse)
                    .WithMany()
                    .HasForeignKey(s => s.WarehouseID);

                entity.HasOne(s => s.MedicationGroup)
                    .WithMany()
                    .HasForeignKey(s => s.MedicationGroupID);

                entity.HasOne(s => s.Shelf)
                    .WithMany()
                    .HasForeignKey(s => s.ShelfID);

                entity.HasOne(s => s.Medication)
                    .WithMany()
                    .HasForeignKey(s => s.MedicationID);
            });

            // StockMovement Beziehungen
            modelBuilder.Entity<StockMovement>(entity =>
            {
                entity.HasOne(sm => sm.MovementReason)
                    .WithMany()
                    .HasForeignKey(sm => sm.MovementReasonID);

                entity.HasOne(sm => sm.Warehouse)
                    .WithMany()
                    .HasForeignKey(sm => sm.WarehouseID);

                entity.HasOne(sm => sm.MedicationGroup)
                    .WithMany()
                    .HasForeignKey(sm => sm.MedicationGroupID);

                entity.HasOne(sm => sm.Shelf)
                    .WithMany()
                    .HasForeignKey(sm => sm.ShelfID);

                entity.HasOne(sm => sm.Medication)
                    .WithMany()
                    .HasForeignKey(sm => sm.MedicationID);

                entity.HasOne(sm => sm.Stock)
                    .WithMany()
                    .HasForeignKey(sm => sm.StockID);
            });

            // Index für eindeutige Namen
            modelBuilder.Entity<Warehouse>()
                .HasIndex(w => w.Name)
                .IsUnique();

            modelBuilder.Entity<MedicationGroup>()
                .HasIndex(mg => mg.SupplyNumber)
                .IsUnique();

            modelBuilder.Entity<MovementReason>()
                .HasIndex(mr => mr.ReasonCode)
                .IsUnique();

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
}