﻿// <auto-generated />
using System;
using CategoryWebsite_MS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CategoryWebsite_MS.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211024230955_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CategoryWebsite_MS.Models.Entities.WebsiteCategory", b =>
                {
                    b.Property<Guid>("WebsiteCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WebsiteCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WebsiteCategoryId");

                    b.ToTable("WebsiteCategories");

                    b.HasData(
                        new
                        {
                            WebsiteCategoryId = new Guid("453ac685-cd85-433a-abd6-2330edaea4f3"),
                            WebsiteCategoryName = "Department Store"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("fd502319-08eb-4e89-bc79-ce9d242563e4"),
                            WebsiteCategoryName = "Speciality Store"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("59ab7e8b-1472-419d-802f-5cda5c7b238d"),
                            WebsiteCategoryName = "Supermarket"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("4ec94f5a-c25b-4db5-8a0b-a5ed1818339c"),
                            WebsiteCategoryName = "Convenience Store"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("2314e0e3-ea11-46a5-9285-d65b29f61420"),
                            WebsiteCategoryName = "Discount Store"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("5c387ba2-fbad-4e59-bfb8-9416811a133b"),
                            WebsiteCategoryName = "Hypermarket"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("459adfd8-2ca5-40a8-bf0c-212bf99fd0de"),
                            WebsiteCategoryName = "Warehouse Store"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("25c50ed0-d381-4154-bbb4-c509487bc505"),
                            WebsiteCategoryName = "E-Commerce"
                        },
                        new
                        {
                            WebsiteCategoryId = new Guid("8a692d9d-e284-4153-aeb9-cc54bcc5ba19"),
                            WebsiteCategoryName = "Drug Store"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
