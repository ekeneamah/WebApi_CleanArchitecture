using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class Updatedclaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimForm",
                table: "Claims",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "ClaimNo",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NotificationNo",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimNo",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "NotificationNo",
                table: "Claims");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Claims",
                newName: "ClaimForm");
        }
    }
}
