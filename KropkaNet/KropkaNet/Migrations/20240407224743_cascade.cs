using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KropkaNet.Migrations
{
    /// <inheritdoc />
    public partial class cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocktakings_StocktakingId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocktakings_Warehouses_WarehouseId",
                table: "Stocktakings");

            migrationBuilder.DropIndex(
                name: "IX_Stocktakings_WarehouseId",
                table: "Stocktakings");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StocktakingId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses",
                column: "StocktakingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_OrderId",
                table: "Stocktakings",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocktakings_Orders_OrderId",
                table: "Stocktakings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Stocktakings_StocktakingId",
                table: "Warehouses",
                column: "StocktakingId",
                principalTable: "Stocktakings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocktakings_Orders_OrderId",
                table: "Stocktakings");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Stocktakings_StocktakingId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Stocktakings_OrderId",
                table: "Stocktakings");

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_WarehouseId",
                table: "Stocktakings",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StocktakingId",
                table: "Orders",
                column: "StocktakingId",
                unique: true,
                filter: "[StocktakingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocktakings_StocktakingId",
                table: "Orders",
                column: "StocktakingId",
                principalTable: "Stocktakings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocktakings_Warehouses_WarehouseId",
                table: "Stocktakings",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
