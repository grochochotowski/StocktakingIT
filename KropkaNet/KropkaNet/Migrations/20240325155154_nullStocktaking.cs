using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KropkaNet.Migrations
{
    /// <inheritdoc />
    public partial class nullStocktaking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocktakings_StocktakingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StocktakingId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "StocktakingId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "StocktakingId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
