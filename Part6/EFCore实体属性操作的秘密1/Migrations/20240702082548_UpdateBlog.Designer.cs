﻿// <auto-generated />
using System;
using EFCore实体属性操作的秘密1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore实体属性操作的秘密1.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240702082548_UpdateBlog")]
    partial class UpdateBlog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EFCore实体属性操作的秘密1.Blog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("EFCore实体属性操作的秘密1.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("EFCore实体属性操作的秘密1.Shop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shop", (string)null);
                });

            modelBuilder.Entity("EFCore实体属性操作的秘密1.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Credit")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passwordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("T_Users", (string)null);
                });

            modelBuilder.Entity("EFCore实体属性操作的秘密1.ValueObject1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ValueObject1s");
                });

            modelBuilder.Entity("EFCore实体属性操作的秘密1.Blog", b =>
                {
                    b.OwnsOne("EFCore实体属性操作的秘密1.MultiLangString", "Body", b1 =>
                        {
                            b1.Property<long>("BlogId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Chinese")
                                .IsUnicode(false)
                                .HasColumnType("varchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("varchar");

                            b1.HasKey("BlogId");

                            b1.ToTable("Blogs");

                            b1.WithOwner()
                                .HasForeignKey("BlogId");
                        });

                    b.OwnsOne("EFCore实体属性操作的秘密1.MultiLangString", "Title", b1 =>
                        {
                            b1.Property<long>("BlogId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Chinese")
                                .HasMaxLength(255)
                                .IsUnicode(false)
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("English")
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)");

                            b1.HasKey("BlogId");

                            b1.ToTable("Blogs");

                            b1.WithOwner()
                                .HasForeignKey("BlogId");
                        });

                    b.Navigation("Body")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("EFCore实体属性操作的秘密1.Shop", b =>
                {
                    b.OwnsOne("EFCore实体属性操作的秘密1.Geo", "Location", b1 =>
                        {
                            b1.Property<long>("ShopId")
                                .HasColumnType("bigint");

                            b1.Property<double>("Latitude")
                                .HasColumnType("float");

                            b1.Property<double>("Longitude")
                                .HasMaxLength(3)
                                .HasColumnType("float");

                            b1.HasKey("ShopId");

                            b1.ToTable("Shop");

                            b1.WithOwner()
                                .HasForeignKey("ShopId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
