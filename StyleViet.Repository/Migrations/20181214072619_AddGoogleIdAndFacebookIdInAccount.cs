using Microsoft.EntityFrameworkCore.Migrations;

namespace StyleViet.Repository.Migrations
{
    public partial class AddGoogleIdAndFacebookIdInAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookId",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Account",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Account");
        }
    }
}
