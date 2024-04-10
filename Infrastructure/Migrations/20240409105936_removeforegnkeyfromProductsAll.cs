using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeforegnkeyfromProductsAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCategoty_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_InsuranceCompany_InsuranceCoyCoy_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryCategoty_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_InsuranceCoyCoy_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryCategoty_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InsuranceCoyCoy_Id",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryCategoty_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceCoyCoy_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCategoty_Id",
                table: "Products",
                column: "CategoryCategoty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InsuranceCoyCoy_Id",
                table: "Products",
                column: "InsuranceCoyCoy_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCategoty_Id",
                table: "Products",
                column: "CategoryCategoty_Id",
                principalTable: "Categories",
                principalColumn: "Categoty_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_InsuranceCompany_InsuranceCoyCoy_Id",
                table: "Products",
                column: "InsuranceCoyCoy_Id",
                principalTable: "InsuranceCompany",
                principalColumn: "Coy_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
