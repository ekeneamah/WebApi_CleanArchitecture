using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedInsuranceCoy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coy_Image",
                table: "InsuranceCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coy_Logo",
                table: "InsuranceCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coy_VideoLink",
                table: "InsuranceCompany",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category_id",
                table: "Benefits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CoyBenefits",
                columns: table => new
                {
                    Benefit_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coy_id = table.Column<int>(type: "int", nullable: false),
                    Benefits_Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoyBenefits", x => x.Benefit_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoyBenefits");

            migrationBuilder.DropColumn(
                name: "Coy_Image",
                table: "InsuranceCompany");

            migrationBuilder.DropColumn(
                name: "Coy_Logo",
                table: "InsuranceCompany");

            migrationBuilder.DropColumn(
                name: "Coy_VideoLink",
                table: "InsuranceCompany");

            migrationBuilder.DropColumn(
                name: "Category_id",
                table: "Benefits");
        }
    }
}
