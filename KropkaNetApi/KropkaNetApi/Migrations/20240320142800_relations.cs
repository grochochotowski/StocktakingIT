using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KropkaNet.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Orders_OrderId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stocktakings_StocktakingId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Companies_CompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocktakings_StocktakingId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Stocktakings_StocktakingId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StocktakingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Employees_StocktakingId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Departments_OrderId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "StocktakingId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "StocktakingId",
                table: "Orders",
                newName: "StockTakingIT");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Orders",
                newName: "DepartmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CompanyId",
                table: "Orders",
                newName: "IX_Orders_DepartmentsId");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Stocktakings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyUser",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUser", x => new { x.CompaniesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CompanyUser_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeStocktaking",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StocktakingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStocktaking", x => new { x.EmployeeId, x.StocktakingsId });
                    table.ForeignKey(
                        name: "FK_EmployeeStocktaking_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeStocktaking_Stocktakings_StocktakingsId",
                        column: x => x.StocktakingsId,
                        principalTable: "Stocktakings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_OrderId",
                table: "Stocktakings",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocktakings_WarehouseId",
                table: "Stocktakings",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_UsersId",
                table: "CompanyUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStocktaking_StocktakingsId",
                table: "EmployeeStocktaking",
                column: "StocktakingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Departments_DepartmentsId",
                table: "Orders",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocktakings_Orders_OrderId",
                table: "Stocktakings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocktakings_Warehouses_WarehouseId",
                table: "Stocktakings",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Departments_DepartmentsId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocktakings_Orders_OrderId",
                table: "Stocktakings");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocktakings_Warehouses_WarehouseId",
                table: "Stocktakings");

            migrationBuilder.DropTable(
                name: "CompanyUser");

            migrationBuilder.DropTable(
                name: "EmployeeStocktaking");

            migrationBuilder.DropIndex(
                name: "IX_Stocktakings_OrderId",
                table: "Stocktakings");

            migrationBuilder.DropIndex(
                name: "IX_Stocktakings_WarehouseId",
                table: "Stocktakings");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Stocktakings");

            migrationBuilder.DropColumn(
                name: "DepartamentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StockTakingIT",
                table: "Orders",
                newName: "StocktakingId");

            migrationBuilder.RenameColumn(
                name: "DepartmentsId",
                table: "Orders",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DepartmentsId",
                table: "Orders",
                newName: "IX_Orders_CompanyId");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StocktakingId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_StocktakingId",
                table: "Warehouses",
                column: "StocktakingId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StocktakingId",
                table: "Orders",
                column: "StocktakingId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StocktakingId",
                table: "Employees",
                column: "StocktakingId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_OrderId",
                table: "Departments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Orders_OrderId",
                table: "Departments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stocktakings_StocktakingId",
                table: "Employees",
                column: "StocktakingId",
                principalTable: "Stocktakings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Companies_CompanyId",
                table: "Orders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocktakings_StocktakingId",
                table: "Orders",
                column: "StocktakingId",
                principalTable: "Stocktakings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Stocktakings_StocktakingId",
                table: "Warehouses",
                column: "StocktakingId",
                principalTable: "Stocktakings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
