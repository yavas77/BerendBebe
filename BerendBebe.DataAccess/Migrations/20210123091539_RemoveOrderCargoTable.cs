using Microsoft.EntityFrameworkCore.Migrations;

namespace BerendBebe.DataAccess.Migrations
{
    public partial class RemoveOrderCargoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderCargos_OrderCargoId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderCargos");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderCargoId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderCargoId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CargoMessage",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoMessage",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CargoName",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderCargoId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderCargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCargos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCargoId",
                table: "Orders",
                column: "OrderCargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderCargos_OrderCargoId",
                table: "Orders",
                column: "OrderCargoId",
                principalTable: "OrderCargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
