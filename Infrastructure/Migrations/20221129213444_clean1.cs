using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class clean1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuranceCoys",
                columns: table => new
                {
                    BrandId = table.Column<int>(name: "Coy_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(name: "Coy_Name", type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(name: "Product_Name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BrandId = table.Column<int>(name: "Coy_Id", type: "int", nullable: true),
                    ProductPrice = table.Column<double>(name: "Product_Price", type: "float", maxLength: 100, nullable: false),
                    ProductQuantity = table.Column<int>(name: "Product_Quantity", type: "int", nullable: true),
                    ProductCode = table.Column<string>(name: "Product_Code", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_Brand_Id",
                        column: x => x.BrandId,
                        principalTable: "InsuranceCoys",
                        principalColumn: "Coy_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Brand_Id",
                table: "Products",
                column: "Coy_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "InsuranceCoys");
        }
    }
}
