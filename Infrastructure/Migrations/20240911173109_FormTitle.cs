using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FormTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProductUnderWritingForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FormId",
                table: "ProductUnderWritingAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ClaimsUnderWritingForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InspectionStatus",
                table: "ClaimsUnderWritingAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ClaimsUnderWritingAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "ClaimsUnderWritingAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ClaimsUnderWritingAnswers_ProductId",
                table: "ClaimsUnderWritingAnswers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimsUnderWritingAnswers_Products_ProductId",
                table: "ClaimsUnderWritingAnswers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimsUnderWritingAnswers_Products_ProductId",
                table: "ClaimsUnderWritingAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ClaimsUnderWritingAnswers_ProductId",
                table: "ClaimsUnderWritingAnswers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProductUnderWritingForms");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ClaimsUnderWritingForms");

            migrationBuilder.DropColumn(
                name: "InspectionStatus",
                table: "ClaimsUnderWritingAnswers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ClaimsUnderWritingAnswers");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "ClaimsUnderWritingAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "FormId",
                table: "ProductUnderWritingAnswers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
