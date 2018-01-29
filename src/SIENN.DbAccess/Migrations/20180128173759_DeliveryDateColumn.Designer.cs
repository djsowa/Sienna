﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SIENN.DbAccess.Context;
using System;

namespace SIENN.DbAccess.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20180128173759_DeliveryDateColumn")]
    partial class DeliveryDateColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("SIENN.DbAccess.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<bool>("IsAvailable");

                    b.Property<DateTime?>("NextDelivery");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductTypeId");

                    b.Property<int>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("UnitId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SIENN.DbAccess.Entity.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SIENN.DbAccess.Entity.ProductToCategory", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("CategoryId");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductsToCategories");
                });

            modelBuilder.Entity("SIENN.DbAccess.Entity.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("SIENN.DbAccess.Entity.ProductUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Unints");
                });

            modelBuilder.Entity("SIENN.DbAccess.Entity.Product", b =>
                {
                    b.HasOne("SIENN.DbAccess.Entity.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SIENN.DbAccess.Entity.ProductUnit", "Unit")
                        .WithMany("Products")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SIENN.DbAccess.Entity.ProductToCategory", b =>
                {
                    b.HasOne("SIENN.DbAccess.Entity.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SIENN.DbAccess.Entity.Product", "Product")
                        .WithMany("Categories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
