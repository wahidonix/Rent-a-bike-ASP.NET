using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT.Migrations
{
    public partial class history9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Bikes_BikeId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_BikeId",
                table: "Histories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Histories_BikeId",
                table: "Histories",
                column: "BikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Bikes_BikeId",
                table: "Histories",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
