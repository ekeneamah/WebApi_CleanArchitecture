using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class removebenefits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryBenefit_Categories_Category_Id",
                table: "CategoryBenefit");

            migrationBuilder.DropIndex(
                name: "IX_CategoryBenefit_Category_Id",
                table: "CategoryBenefit");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "CategoryBenefit");

            migrationBuilder.AlterColumn<string>(
                name: "Benefits_Title",
                table: "CategoryBenefit",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Benefits_Title",
                table: "CategoryBenefit",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "CategoryBenefit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBenefit_Category_Id",
                table: "CategoryBenefit",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryBenefit_Categories_Category_Id",
                table: "CategoryBenefit",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Category_Id");
        }
    }
}
