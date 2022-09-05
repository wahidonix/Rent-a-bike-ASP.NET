using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT.Migrations
{
    public partial class Bikes4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Stations_StationId",
                table: "Bikes");

            migrationBuilder.DropIndex(
                name: "IX_Bikes_StationId",
                table: "Bikes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bikes_StationId",
                table: "Bikes",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Stations_StationId",
                table: "Bikes",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
