﻿// <auto-generated />
using MediManager_WebApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediManager_WebApp.Migrations
{
    [DbContext(typeof(MediManagerDbContext))]
    [Migration("20241227204852_AddSeedData")]
    partial class AddSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediManager_WebApp.Models.AuthenticationLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean");

                    b.Property<string>("Timestamp")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.ToTable("AuthenticationLogs");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.ExportLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("ExportType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FileFormat")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("RecordCount")
                        .HasColumnType("integer");

                    b.Property<string>("Timestamp")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("ExportLogs");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Medication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("MedicationGroupID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("PZN")
                        .HasColumnType("integer");

                    b.Property<string>("SAPName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("SAPNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.HasIndex("MedicationGroupID");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.MedicationGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("QuantityUnitID")
                        .HasColumnType("integer");

                    b.Property<string>("SupplyNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.HasIndex("QuantityUnitID");

                    b.ToTable("MedicationGroups");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.MovementReason", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("MovementType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ReasonCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("ID");

                    b.ToTable("MovementReasons");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "Abgabe an Patient",
                            MovementType = "OUT",
                            ReasonCode = "PATIENT_HANDOUT"
                        },
                        new
                        {
                            ID = 2,
                            Description = "Vernichtung",
                            MovementType = "OUT",
                            ReasonCode = "DESTRUCTION"
                        },
                        new
                        {
                            ID = 3,
                            Description = "Verfallen",
                            MovementType = "OUT",
                            ReasonCode = "EXPIRED"
                        },
                        new
                        {
                            ID = 4,
                            Description = "Umlagerung (Ausgang)",
                            MovementType = "OUT",
                            ReasonCode = "WAREHOUSE_TRANSFER_OUT"
                        },
                        new
                        {
                            ID = 5,
                            Description = "Umlagerung (Eingang)",
                            MovementType = "IN",
                            ReasonCode = "WAREHOUSE_TRANSFER_IN"
                        },
                        new
                        {
                            ID = 6,
                            Description = "Neue Lieferung",
                            MovementType = "IN",
                            ReasonCode = "NEW_STOCK"
                        },
                        new
                        {
                            ID = 7,
                            Description = "Korrektur (Zugang)",
                            MovementType = "IN",
                            ReasonCode = "CORRECTION_IN"
                        },
                        new
                        {
                            ID = 8,
                            Description = "Korrektur (Abgang)",
                            MovementType = "OUT",
                            ReasonCode = "CORRECTION_OUT"
                        });
                });

            modelBuilder.Entity("MediManager_WebApp.Models.QuantityUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.ToTable("QuantityUnits");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "Einzelne Einheit (EA)",
                            Name = "Stück"
                        },
                        new
                        {
                            ID = 2,
                            Description = "Flüssigkeiten in Flasche (BT)",
                            Name = "Flasche"
                        },
                        new
                        {
                            ID = 3,
                            Description = "Flüssigkeitsmenge in ml",
                            Name = "Milliliter"
                        },
                        new
                        {
                            ID = 4,
                            Description = "Gewicht in g",
                            Name = "Gramm"
                        });
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Shelf", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Stock", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Batch")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ExpireDate")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("MedicationGroupID")
                        .HasColumnType("integer");

                    b.Property<int>("MedicationID")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("ShelfID")
                        .HasColumnType("integer");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("MedicationGroupID");

                    b.HasIndex("MedicationID");

                    b.HasIndex("ShelfID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.StockMovement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Batch")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ExpireDate")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("MedicationGroupID")
                        .HasColumnType("integer");

                    b.Property<int>("MedicationID")
                        .HasColumnType("integer");

                    b.Property<int>("MovementReasonID")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("ShelfID")
                        .HasColumnType("integer");

                    b.Property<int>("StockID")
                        .HasColumnType("integer");

                    b.Property<string>("Timestamp")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("MedicationGroupID");

                    b.HasIndex("MedicationID");

                    b.HasIndex("MovementReasonID");

                    b.HasIndex("ShelfID");

                    b.HasIndex("StockID");

                    b.HasIndex("UserID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("StockMovements");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.SystemLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("ObjectID")
                        .HasColumnType("integer");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Timestamp")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("SystemLogs");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("ForcePasswordChange")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.UserSetting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<bool>("EmailNotifications")
                        .HasColumnType("boolean");

                    b.Property<bool>("PopupNotifications")
                        .HasColumnType("boolean");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Warehouse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ID");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.WarehouseMedicationGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("MedicationGroupID")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("MedicationGroupID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("WarehouseMedicationGroups");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.ExportLog", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Medication", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.MedicationGroup", "MedicationGroup")
                        .WithMany()
                        .HasForeignKey("MedicationGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicationGroup");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.MedicationGroup", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.QuantityUnit", "QuantityUnit")
                        .WithMany()
                        .HasForeignKey("QuantityUnitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuantityUnit");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Shelf", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Stock", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.MedicationGroup", "MedicationGroup")
                        .WithMany()
                        .HasForeignKey("MedicationGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Medication", "Medication")
                        .WithMany()
                        .HasForeignKey("MedicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Shelf", "Shelf")
                        .WithMany()
                        .HasForeignKey("ShelfID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("MedicationGroup");

                    b.Navigation("Shelf");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.StockMovement", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.MedicationGroup", "MedicationGroup")
                        .WithMany()
                        .HasForeignKey("MedicationGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Medication", "Medication")
                        .WithMany()
                        .HasForeignKey("MedicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.MovementReason", "MovementReason")
                        .WithMany()
                        .HasForeignKey("MovementReasonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Shelf", "Shelf")
                        .WithMany()
                        .HasForeignKey("ShelfID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("MedicationGroup");

                    b.Navigation("MovementReason");

                    b.Navigation("Shelf");

                    b.Navigation("Stock");

                    b.Navigation("User");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.SystemLog", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.UserSetting", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("MediManager_WebApp.Models.UserSetting", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.WarehouseMedicationGroup", b =>
                {
                    b.HasOne("MediManager_WebApp.Models.MedicationGroup", "MedicationGroup")
                        .WithMany()
                        .HasForeignKey("MedicationGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediManager_WebApp.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicationGroup");

                    b.Navigation("Warehouse");
                });
#pragma warning restore 612, 618
        }
    }
}
