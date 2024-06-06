using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KropkaNet.Migrations
{
    /// <inheritdoc />
    public partial class nullStocktakingId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses");

            migrationBuilder.AlterColumn<int>(
                name: "StocktakingId",
                table: "Warehouses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Stocktakings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses",
                column: "StocktakingId",
                unique: true,
                filter: "[StocktakingId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses");

            migrationBuilder.AlterColumn<int>(
                name: "StocktakingId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Stocktakings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses",
                column: "StocktakingId",
                unique: true);
        }
    }
}
