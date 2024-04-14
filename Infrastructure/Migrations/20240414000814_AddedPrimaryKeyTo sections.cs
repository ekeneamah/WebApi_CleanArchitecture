using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrimaryKeyTosections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Categories_CategoryCategoty_Id",
                table: "Benefits");

            migrationBuilder.RenameColumn(
                name: "CategoryCategoty_Id",
                table: "Benefits",
                newName: "CategoryEntityCategoty_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Benefits_CategoryCategoty_Id",
                table: "Benefits",
                newName: "IX_Benefits_CategoryEntityCategoty_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Categories_CategoryEntityCategoty_Id",
                table: "Benefits",
                column: "CategoryEntityCategoty_Id",
                principalTable: "Categories",
                principalColumn: "Categoty_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Categories_CategoryEntityCategoty_Id",
                table: "Benefits");

            migrationBuilder.RenameColumn(
                name: "CategoryEntityCategoty_Id",
                table: "Benefits",
                newName: "CategoryCategoty_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Benefits_CategoryEntityCategoty_Id",
                table: "Benefits",
                newName: "IX_Benefits_CategoryCategoty_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Categories_CategoryCategoty_Id",
                table: "Benefits",
                column: "CategoryCategoty_Id",
                principalTable: "Categories",
                principalColumn: "Categoty_Id");
        }
    }
}
