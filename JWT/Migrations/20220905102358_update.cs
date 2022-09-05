using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "verified",
                table: "Users",
                newName: "Verified");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Verified",
                table: "Users",
                newName: "verified");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");
        }
    }
}
