using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CoyProductUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormId",
                table: "ProductUnderWritingAnswers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "InspectionStatus",
                table: "ProductUnderWritingAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductUnderWritingAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "ProductUnderWritingAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CoyProductId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequireInspection",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProvidesQuestionForm",
                table: "InsuranceCompany",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnderWritingAnswers_ProductId",
                table: "ProductUnderWritingAnswers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnderWritingAnswers_Products_ProductId",
                table: "ProductUnderWritingAnswers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnderWritingAnswers_Products_ProductId",
                table: "ProductUnderWritingAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ProductUnderWritingAnswers_ProductId",
                table: "ProductUnderWritingAnswers");

            migrationBuilder.DropColumn(
                name: "InspectionStatus",
                table: "ProductUnderWritingAnswers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductUnderWritingAnswers");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "ProductUnderWritingAnswers");

            migrationBuilder.DropColumn(
                name: "CoyProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RequireInspection",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProvidesQuestionForm",
                table: "InsuranceCompany");

            migrationBuilder.AlterColumn<string>(
                name: "FormId",
                table: "ProductUnderWritingAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
