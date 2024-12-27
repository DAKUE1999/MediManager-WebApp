using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediManager_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MovementReasons",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "QuantityUnits",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuantityUnits",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QuantityUnits",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuantityUnits",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
