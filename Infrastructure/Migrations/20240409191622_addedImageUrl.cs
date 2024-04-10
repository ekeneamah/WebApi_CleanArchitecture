using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Category_Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<string>(
                name: "Category_Image",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    Benefit_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Benefits_Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryCategoty_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.Benefit_Id);
                    table.ForeignKey(
                        name: "FK_Benefits_Categories_CategoryCategoty_Id",
                        column: x => x.CategoryCategoty_Id,
                        principalTable: "Categories",
                        principalColumn: "Categoty_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benefits_CategoryCategoty_Id",
                table: "Benefits",
                column: "CategoryCategoty_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropColumn(
                name: "Category_Image",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Category_Description",
                table: "Categories",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
