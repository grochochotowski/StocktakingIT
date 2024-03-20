using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KropkaNet.Migrations
{
    /// <inheritdoc />
    public partial class namingv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Departments_DepartmentsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DepartamentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DepartmentsId",
                table: "Orders",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DepartmentsId",
                table: "Orders",
                newName: "IX_Orders_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Departments_DepartmentId",
                table: "Orders",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Departments_DepartmentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Orders",
                newName: "DepartmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DepartmentId",
                table: "Orders",
                newName: "IX_Orders_DepartmentsId");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Departments_DepartmentsId",
                table: "Orders",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
