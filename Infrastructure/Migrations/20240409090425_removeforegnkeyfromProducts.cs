using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeforegnkeyfromProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Category_Id",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryCategoty_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCategoty_Id",
                table: "Products",
                column: "CategoryCategoty_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCategoty_Id",
                table: "Products",
                column: "CategoryCategoty_Id",
                principalTable: "Categories",
                principalColumn: "Categoty_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCategoty_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryCategoty_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryCategoty_Id",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Categoty_Id");
        }
    }
}
