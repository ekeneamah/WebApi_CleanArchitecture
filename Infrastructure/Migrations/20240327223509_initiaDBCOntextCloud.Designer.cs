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
    [Migration("20240327223509_initiaDBCOntextCloud")]
    partial class initiaDBCOntextCloud
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.InsuranceCoys", b =>
                {
                    b.Property<int>("Coy_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Coy_Id"));

                    b.Property<string>("Coy_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Coy_Id");

                    b.ToTable("InsuranceCoys");
                });

            modelBuilder.Entity("Domain.Models.Category", b =>
                {
                    b.Property<int>("Categoty_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Categoty_Id"));

                    b.Property<string>("Category_Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Categoty_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Models.Product", b =>
                {
                    b.Property<int>("Product_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_Id"));

                    b.Property<int>("Coy_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Categoty_Id")
                        .HasColumnType("int");

                    b.Property<string>("Product_Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Product_Price")
                        .HasMaxLength(100)
                        .HasColumnType("float");

                    b.Property<int>("Product_Quantity")
                        .HasColumnType("int");

                    b.HasKey("Product_Id");

                    b.HasIndex("Coy_Id");

                    b.HasIndex("Categoty_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Models.UserProfile", b =>
                {
                    b.Property<int>("Profile_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Profile_Id"));

                    b.Property<string>("BVN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateofBirth")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("Maidenname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NIN")
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

                    b.HasKey("Profile_Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Domain.Models.Product", b =>
                {
                    b.HasOne("Domain.Models.InsuranceCoys", "InsuranceCoys")
                        .WithMany()
                        .HasForeignKey("Coy_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Categoty_Id");

                    b.Navigation("InsuranceCoys");

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
