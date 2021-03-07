using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpirationDestroyerBlazorServer.DataAccess.Migrations
{
    public partial class RemoveExpiredColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expired",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
