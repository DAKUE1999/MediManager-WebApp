﻿// <auto-generated />
using MediManager_WebApp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediManager_WebApp.Migrations
{
    [DbContext(typeof(MediManagerDbContext))]
    partial class MediManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediManager_WebApp.Models.Medication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Manufacturer")
                        .HasColumnType("text");

                    b.Property<int>("MedicationGroupID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PZN")
                        .HasColumnType("integer");

                    b.Property<string>("SAPName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SAPNumber")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("text");

                    b.Property<int>("QuantityUnitID")
                        .HasColumnType("integer");

                    b.Property<string>("SupplyNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("QuantityUnitID");

                    b.HasIndex("SupplyNumber")
                        .IsUnique();

                    b.ToTable("MedicationGroups");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.MovementReason", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("MovementType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReasonCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ReasonCode")
                        .IsUnique();

                    b.ToTable("MovementReasons");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.QuantityUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("QuantityUnits");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Shelf", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("text");

                    b.Property<string>("ExpireDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MedicationGroupID")
                        .HasColumnType("integer");

                    b.Property<int>("MedicationID")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

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
                        .HasColumnType("text");

                    b.Property<string>("ExpireDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MedicationGroupID")
                        .HasColumnType("integer");

                    b.Property<int>("MedicationID")
                        .HasColumnType("integer");

                    b.Property<int>("MovementReasonID")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<int>("ShelfID")
                        .HasColumnType("integer");

                    b.Property<int>("StockID")
                        .HasColumnType("integer");

                    b.Property<string>("Timestamp")
                        .IsRequired()
                        .HasColumnType("text");

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

            modelBuilder.Entity("MediManager_WebApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("ForcePasswordChange")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MediManager_WebApp.Models.Warehouse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Warehouses");
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
                        .WithMany("Shelves")
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

            modelBuilder.Entity("MediManager_WebApp.Models.Warehouse", b =>
                {
                    b.Navigation("Shelves");
                });
#pragma warning restore 612, 618
        }
    }
}
