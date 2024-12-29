using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediManager_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovementReasons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReasonCode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MovementType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementReasons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QuantityUnits",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityUnits", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: false),
                    ForcePasswordChange = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MedicationGroups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuantityUnitID = table.Column<int>(type: "integer", nullable: false),
                    SupplyNumber = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedicationGroups_QuantityUnits_QuantityUnitID",
                        column: x => x.QuantityUnitID,
                        principalTable: "QuantityUnits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shelves",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shelves_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicationGroupID = table.Column<int>(type: "integer", nullable: false),
                    PZN = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    SAPNumber = table.Column<string>(type: "text", nullable: false),
                    SAPName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medications_MedicationGroups_MedicationGroupID",
                        column: x => x.MedicationGroupID,
                        principalTable: "MedicationGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseMedicationGroups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseID = table.Column<int>(type: "integer", nullable: false),
                    MedicationGroupID = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMedicationGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WarehouseMedicationGroups_MedicationGroups_MedicationGroupID",
                        column: x => x.MedicationGroupID,
                        principalTable: "MedicationGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseMedicationGroups_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseID = table.Column<int>(type: "integer", nullable: false),
                    MedicationGroupID = table.Column<int>(type: "integer", nullable: false),
                    ShelfID = table.Column<int>(type: "integer", nullable: false),
                    MedicationID = table.Column<int>(type: "integer", nullable: false),
                    Batch = table.Column<string>(type: "text", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stocks_MedicationGroups_MedicationGroupID",
                        column: x => x.MedicationGroupID,
                        principalTable: "MedicationGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Shelves_ShelfID",
                        column: x => x.ShelfID,
                        principalTable: "Shelves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseID = table.Column<int>(type: "integer", nullable: false),
                    MedicationGroupID = table.Column<int>(type: "integer", nullable: false),
                    ShelfID = table.Column<int>(type: "integer", nullable: false),
                    MedicationID = table.Column<int>(type: "integer", nullable: false),
                    StockID = table.Column<int>(type: "integer", nullable: false),
                    MovementReasonID = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Batch = table.Column<string>(type: "text", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Timestamp = table.Column<string>(type: "text", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockMovements_MedicationGroups_MedicationGroupID",
                        column: x => x.MedicationGroupID,
                        principalTable: "MedicationGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_MovementReasons_MovementReasonID",
                        column: x => x.MovementReasonID,
                        principalTable: "MovementReasons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_Shelves_ShelfID",
                        column: x => x.ShelfID,
                        principalTable: "Shelves",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_Stocks_StockID",
                        column: x => x.StockID,
                        principalTable: "Stocks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MovementReasons",
                columns: new[] { "ID", "Description", "MovementType", "ReasonCode" },
                values: new object[,]
                {
                    { 1, "Abgabe an Patient", "OUT", "PATIENT_HANDOUT" },
                    { 2, "Vernichtung", "OUT", "DESTRUCTION" },
                    { 3, "Verfallen", "OUT", "EXPIRED" },
                    { 4, "Umlagerung (Ausgang)", "OUT", "WAREHOUSE_TRANSFER_OUT" },
                    { 5, "Umlagerung (Eingang)", "IN", "WAREHOUSE_TRANSFER_IN" },
                    { 6, "Neue Lieferung", "IN", "NEW_STOCK" },
                    { 7, "Korrektur (Zugang)", "IN", "CORRECTION_IN" },
                    { 8, "Korrektur (Abgang)", "OUT", "CORRECTION_OUT" }
                });

            migrationBuilder.InsertData(
                table: "QuantityUnits",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Einzelne Einheit (EA)", "Stück" },
                    { 2, "Flüssigkeiten in Flasche (BT)", "Flasche" },
                    { 3, "Flüssigkeitsmenge in ml", "Milliliter" },
                    { 4, "Gewicht in g", "Gramm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationGroups_QuantityUnitID",
                table: "MedicationGroups",
                column: "QuantityUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationGroups_SupplyNumber",
                table: "MedicationGroups",
                column: "SupplyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_MedicationGroupID",
                table: "Medications",
                column: "MedicationGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_MovementReasons_ReasonCode",
                table: "MovementReasons",
                column: "ReasonCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_WarehouseID",
                table: "Shelves",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MedicationGroupID",
                table: "StockMovements",
                column: "MedicationGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MedicationID",
                table: "StockMovements",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MovementReasonID",
                table: "StockMovements",
                column: "MovementReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ShelfID",
                table: "StockMovements",
                column: "ShelfID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_StockID",
                table: "StockMovements",
                column: "StockID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_UserID",
                table: "StockMovements",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_WarehouseID",
                table: "StockMovements",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_MedicationGroupID",
                table: "Stocks",
                column: "MedicationGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_MedicationID",
                table: "Stocks",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ShelfID",
                table: "Stocks",
                column: "ShelfID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WarehouseID",
                table: "Stocks",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMedicationGroups_MedicationGroupID",
                table: "WarehouseMedicationGroups",
                column: "MedicationGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMedicationGroups_WarehouseID",
                table: "WarehouseMedicationGroups",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_Name",
                table: "Warehouses",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "WarehouseMedicationGroups");

            migrationBuilder.DropTable(
                name: "MovementReasons");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropTable(
                name: "MedicationGroups");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "QuantityUnits");
        }
    }
}
