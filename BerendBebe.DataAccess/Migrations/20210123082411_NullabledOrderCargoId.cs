using Microsoft.EntityFrameworkCore.Migrations;

namespace BerendBebe.DataAccess.Migrations
{
    public partial class NullabledOrderCargoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderCargos_OrderCargoId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderCargoId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderCargos_OrderCargoId",
                table: "Orders",
                column: "OrderCargoId",
                principalTable: "OrderCargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderCargos_OrderCargoId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderCargoId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderCargos_OrderCargoId",
                table: "Orders",
                column: "OrderCargoId",
                principalTable: "OrderCargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
