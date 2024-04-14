using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifiedinrancecoy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_InsuranceCompany_InsuranceCoyCoy_Id",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Products_ProductId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_UserProfiles_UserProfileProfile_Id",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_InsuranceCoyCoy_Id",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_ProductId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_UserProfileProfile_Id",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "InsuranceCoyCoy_Id",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "UserProfileProfile_Id",
                table: "Policies");

            migrationBuilder.AlterColumn<string>(
                name: "NIN",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NIN",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceCoyCoy_Id",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileProfile_Id",
                table: "Policies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_InsuranceCoyCoy_Id",
                table: "Policies",
                column: "InsuranceCoyCoy_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ProductId",
                table: "Policies",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_UserProfileProfile_Id",
                table: "Policies",
                column: "UserProfileProfile_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_InsuranceCompany_InsuranceCoyCoy_Id",
                table: "Policies",
                column: "InsuranceCoyCoy_Id",
                principalTable: "InsuranceCompany",
                principalColumn: "Coy_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Products_ProductId",
                table: "Policies",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_UserProfiles_UserProfileProfile_Id",
                table: "Policies",
                column: "UserProfileProfile_Id",
                principalTable: "UserProfiles",
                principalColumn: "Profile_Id");
        }
    }
}
