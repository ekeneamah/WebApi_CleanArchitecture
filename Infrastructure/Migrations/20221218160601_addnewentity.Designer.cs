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
    [Migration("20221218160601_addnewentity")]
    partial class Addnewentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.InsuranceCoys", b =>
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

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("Categoty_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Categoty_Id"));

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Categoty_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Product", b =>
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

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.HasOne("Domain.InsuranceCoys", "InsuranceCoys")
                        .WithMany()
                        .HasForeignKey("Coy_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Categoty_Id");

                    b.Navigation("InsuranceCoys");

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
