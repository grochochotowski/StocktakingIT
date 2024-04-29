using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KropkaNet.Migrations
{
    /// <inheritdoc />
    public partial class namingv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocktakings_Orders_OrderId",
                table: "Stocktakings");

            migrationBuilder.DropIndex(
                name: "IX_Stocktakings_OrderId",
                table: "Stocktakings");

            migrationBuilder.RenameColumn(
                name: "StocktakingIT",
                table: "Orders",
                newName: "StocktakingId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StocktakingId",
                table: "Orders",
                column: "StocktakingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocktakings_StocktakingId",
                table: "Orders",
                column: "StocktakingId",
                principalTable: "Stocktakings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocktakings_StocktakingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StocktakingId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StocktakingId",
                table: "Orders",
                newName: "StocktakingIT");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
