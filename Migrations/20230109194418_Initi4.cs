using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WajedApi.Migrations
{
    public partial class Initi4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DefaultAddress",
                table: "Addresses",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultAddress",
                table: "Addresses");
        }
    }
}
