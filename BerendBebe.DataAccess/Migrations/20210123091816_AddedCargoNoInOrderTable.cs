using Microsoft.EntityFrameworkCore.Migrations;

namespace BerendBebe.DataAccess.Migrations
{
    public partial class AddedCargoNoInOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CargoNo",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoNo",
                table: "Orders");
        }
    }
}
