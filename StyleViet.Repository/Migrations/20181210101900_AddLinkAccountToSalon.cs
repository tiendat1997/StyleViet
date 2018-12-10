using Microsoft.EntityFrameworkCore.Migrations;

namespace StyleViet.Repository.Migrations
{
    public partial class AddLinkAccountToSalon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Salon",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salon_AccountId",
                table: "Salon",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salon_Account_AccountId",
                table: "Salon",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salon_Account_AccountId",
                table: "Salon");

            migrationBuilder.DropIndex(
                name: "IX_Salon_AccountId",
                table: "Salon");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Salon");
        }
    }
}
