﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category_VideoLink = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Category_Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryandInsurancecoys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceCoyId = table.Column<int>(type: "int", nullable: false),
                    InsuranceCoyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryandInsurancecoys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryBenefit",
                columns: table => new
                {
                    Benefit_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Benefits_Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Benefit_Category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBenefit", x => x.Benefit_Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LossDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotifyDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceCompanyId = table.Column<int>(type: "int", nullable: false),
                    NotificationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimsForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APIEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coy_id = table.Column<int>(type: "int", nullable: false),
                    Coy_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsForms", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "InsuranceCompany",
                columns: table => new
                {
                    Coy_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coy_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Coy_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coy_Status = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Coy_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coy_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Coy_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Coy_Phone = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Coy_PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Coy_State = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Coy_Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Coy_ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Coy_CityCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Coy_CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Coy_VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coy_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coy_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coy_AgentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOrg = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCompany", x => x.Coy_Id);
                });

            migrationBuilder.CreateTable(
                name: "KYCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotorClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleRegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccidentDescribe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccidentPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LossTypeCode = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coy_Id = table.Column<int>(type: "int", nullable: false),
                    PolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentRef = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyGenReturnedData_cornerstone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    documentNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    policyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    naicomID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sumInsured = table.Column<int>(type: "int", nullable: false),
                    premium = table.Column<int>(type: "int", nullable: false),
                    fxRate = table.Column<int>(type: "int", nullable: false),
                    fxCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyGenReturnedData_cornerstone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicySectionFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicySection_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySectionFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicySectionRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicySection_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySectionRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "policySections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyGenReturnedData_cornerstone_Id = table.Column<int>(type: "int", nullable: false),
                    sectionSumInsured = table.Column<double>(type: "float", nullable: false),
                    sectionPremium = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policySections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicySectionSmis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicySection_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sumInsured = table.Column<int>(type: "int", nullable: false),
                    premium = table.Column<int>(type: "int", nullable: false),
                    premiumRate = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySectionSmis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Coy_Id = table.Column<int>(type: "int", nullable: false),
                    Category_Id = table.Column<int>(type: "int", nullable: true),
                    Product_Price = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    Product_Quantity = table.Column<int>(type: "int", nullable: false),
                    Product_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Approved = table.Column<bool>(type: "bit", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true),
                    Is_Recommended = table.Column<bool>(type: "bit", nullable: false),
                    SortingWeight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Reference = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Authorization_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    PolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentRef = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Reference);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Profile_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateofBirth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ResidentialAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidentPerminNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maidenname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stateoforigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BVN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Profile_Id);
                });

            migrationBuilder.CreateTable(
                name: "VehiclePremiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Premium = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SumInsured = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePremiums", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryandInsurancecoys");

            migrationBuilder.DropTable(
                name: "CategoryBenefit");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ClaimsForms");

            migrationBuilder.DropTable(
                name: "CoyBenefits");

            migrationBuilder.DropTable(
                name: "InsuranceCompany");

            migrationBuilder.DropTable(
                name: "KYCs");

            migrationBuilder.DropTable(
                name: "MotorClaims");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "PolicyGenReturnedData_cornerstone");

            migrationBuilder.DropTable(
                name: "PolicySectionFields");

            migrationBuilder.DropTable(
                name: "PolicySectionRates");

            migrationBuilder.DropTable(
                name: "policySections");

            migrationBuilder.DropTable(
                name: "PolicySectionSmis");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "VehiclePremiums");
        }
    }
}