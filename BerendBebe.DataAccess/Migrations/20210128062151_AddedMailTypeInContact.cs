using Microsoft.EntityFrameworkCore.Migrations;

namespace BerendBebe.DataAccess.Migrations
{
    public partial class AddedMailTypeInContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MailType",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailType",
                table: "Contacts");
        }
    }
}
