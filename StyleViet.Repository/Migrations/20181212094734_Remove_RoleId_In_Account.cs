using Microsoft.EntityFrameworkCore.Migrations;

namespace StyleViet.Repository.Migrations
{
    public partial class Remove_RoleId_In_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Account");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Account",
                nullable: false,
                defaultValue: 0);
        }
    }
}
