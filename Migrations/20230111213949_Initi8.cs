using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WajedApi.Migrations
{
    public partial class Initi8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "restaurantId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "restaurantId",
                table: "Carts");
        }
    }
}
