﻿// <auto-generated />
using System;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240815221355_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CategoryVideoLink")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.CategoryBenefit", b =>
                {
                    b.Property<int>("BenefitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenefitId"));

                    b.Property<int>("BenefitCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("BenefitsTitle")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("BenefitId");

                    b.ToTable("CategoryBenefits");
                });

            modelBuilder.Entity("Domain.Entities.CategoryandInsurancecoy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InsuranceCoyId")
                        .HasColumnType("int");

                    b.Property<string>("CoyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoryandInsurancecoys");
                });

            modelBuilder.Entity("Domain.Entities.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ClaimsId")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("InsuranceCompanyId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("LossDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotifyDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("Domain.Entities.ClaimsForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApiEndPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CoyId")
                        .HasColumnType("int");

                    b.Property<string>("CoyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClaimsForms");
                });

            modelBuilder.Entity("Domain.Entities.CoyBenefitEntity", b =>
                {
                    b.Property<int>("BenefitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenefitId"));

                    b.Property<string>("BenefitsTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CoyId")
                        .HasColumnType("int");

                    b.HasKey("BenefitId");

                    b.ToTable("CoyBenefits");
                });

            modelBuilder.Entity("Domain.Entities.InsuranceCoy", b =>
                {
                    b.Property<int>("CoyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoyId"));

                    b.Property<string>("CoyAgentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoyCity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CoyCityCode")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CoyCountry")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CoyCountryCode")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CoyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoyImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoyLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CoyPhone")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("CoyPostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CoyState")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CoyStatus")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("CoyStreet")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CoyVideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoyZipCode")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<bool>("IsOrg")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CoyId");

                    b.ToTable("InsuranceCompany");
                });

            modelBuilder.Entity("Domain.Entities.MotorClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AccidentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AccidentDescribe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccidentPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LossTypeCode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleRegNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WeatherCondition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MotorClaims");
                });

            modelBuilder.Entity("Domain.Entities.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoyId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentRef")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchasedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionRef")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Domain.Entities.PolicyGenReturnedDataCornerstone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AgentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certificate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FxCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FxRate")
                        .HasColumnType("int");

                    b.Property<string>("NaicomId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Premium")
                        .HasColumnType("int");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SumInsured")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PolicyGenReturnedData_cornerstone");
                });

            modelBuilder.Entity("Domain.Entities.PolicySection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PolicyGenReturnedDataCornerstoneId")
                        .HasColumnType("int");

                    b.Property<double>("SectionPremium")
                        .HasColumnType("float");

                    b.Property<double>("SectionSumInsured")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("policySections");
                });

            modelBuilder.Entity("Domain.Entities.PolicySectionField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PolicySectionId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PolicySectionFields");
                });

            modelBuilder.Entity("Domain.Entities.PolicySectionRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PolicySectionId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PolicySectionRates");
                });

            modelBuilder.Entity("Domain.Entities.PolicySectionSmi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PolicySectionId")
                        .HasColumnType("int");

                    b.Property<int>("Premium")
                        .HasColumnType("int");

                    b.Property<int>("PremiumRate")
                        .HasColumnType("int");

                    b.Property<int>("SumInsured")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PolicySectionSmis");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CoyId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRecommended")
                        .HasColumnType("bit");

                    b.Property<int>("PriceType")
                        .HasColumnType("int");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("ProductPrice")
                        .HasMaxLength(100)
                        .HasColumnType("float");

                    b.Property<double>("ProductPricePercentatage")
                        .HasColumnType("float");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("SortingWeight")
                        .HasColumnType("int");

                    b.Property<string>("ThumbNail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.ProductBenefit", b =>
                {
                    b.Property<int>("BenefitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenefitId"));

                    b.Property<string>("BenefitProductId")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("BenefitsTitle")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("BenefitId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductBenefits");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccessCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("AuthorizationUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Reference");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.UserProfile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfileId"));

                    b.Property<string>("BusinessLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bvn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateofBirth")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maidenname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Nin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResidentPerminNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResidentialAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignatureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stateoforigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProfileId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Domain.Entities.VehiclePremium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Premium")
                        .HasColumnType("float");

                    b.Property<string>("SumInsured")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleClass")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("VehiclePremiums");
                });

            modelBuilder.Entity("Domain.Entities.kyc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FromExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KYCs");
                });

            modelBuilder.Entity("Domain.Entities.ProductBenefit", b =>
                {
                    b.HasOne("Domain.Entities.Product", null)
                        .WithMany("Benefit")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("Benefit");
                });
#pragma warning restore 612, 618
        }
    }
}
