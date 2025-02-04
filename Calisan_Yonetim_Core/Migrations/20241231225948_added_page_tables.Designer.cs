﻿// <auto-generated />
using System;
using Calisan_Yonetim_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    [DbContext(typeof(CalisanYonetimDbContext))]
    [Migration("20241231225948_added_page_tables")]
    partial class added_page_tables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyStatus")
                        .HasColumnType("int");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");

                    b.HasData(
                        new
                        {
                            CompanyId = new Guid("58c2f602-afa9-4093-a9b2-49436cd34707"),
                            CompanyName = "Pau",
                            CompanyStatus = 1
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Page", b =>
                {
                    b.Property<Guid>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PageDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("PageId");

                    b.ToTable("Pages");

                    b.HasData(
                        new
                        {
                            PageId = new Guid("e1fd8f1d-18c7-45c0-95a0-cdf0d4fd93c1"),
                            PageDescription = "",
                            PageName = "Company",
                            Status = 1
                        },
                        new
                        {
                            PageId = new Guid("bd07b955-61b4-4850-8db2-54fbffabb0a1"),
                            PageDescription = "",
                            PageName = "Pages",
                            Status = 1
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Personnel", b =>
                {
                    b.Property<Guid>("PersonnelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("PersonnelId");

                    b.ToTable("Personnel");

                    b.HasData(
                        new
                        {
                            PersonnelId = new Guid("340385ba-7e11-484d-b4ab-74b6af7f4cce"),
                            Email = "admin@pau.net",
                            FistName = "Admin",
                            LastName = "Pau",
                            Status = 1
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("RoleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"),
                            RoleName = "System Admin",
                            Status = 1
                        },
                        new
                        {
                            RoleId = new Guid("13ca689f-7d8b-45e9-95d7-b942992aa8f2"),
                            RoleName = "Admin",
                            Status = 1
                        },
                        new
                        {
                            RoleId = new Guid("bc32ddd6-3c27-49a4-b8bb-3d7e31516c72"),
                            RoleName = "User",
                            Status = 1
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.RolePage", b =>
                {
                    b.Property<Guid>("RolePageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("RolePageId");

                    b.HasIndex("PageId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePages");

                    b.HasData(
                        new
                        {
                            RolePageId = new Guid("9e187bab-743a-43f5-a7ee-7c0dbca3984c"),
                            PageId = new Guid("e1fd8f1d-18c7-45c0-95a0-cdf0d4fd93c1"),
                            RoleId = new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"),
                            Status = 1
                        },
                        new
                        {
                            RolePageId = new Guid("94c3cc3e-2f4f-4737-a6ee-957cd1041125"),
                            PageId = new Guid("bd07b955-61b4-4850-8db2-54fbffabb0a1"),
                            RoleId = new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"),
                            Status = 1
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PersonnelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PersonnelId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e141280d-f6c4-4e6b-b9a1-c49761d6f9ce"),
                            CompanyId = new Guid("58c2f602-afa9-4093-a9b2-49436cd34707"),
                            Password = "admin123",
                            PersonnelId = new Guid("340385ba-7e11-484d-b4ab-74b6af7f4cce"),
                            Status = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserRoleId = new Guid("e0162d14-9dc9-4ce8-a1f5-e6eaed2b411f"),
                            RoleId = new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"),
                            Status = 1,
                            UserId = new Guid("e141280d-f6c4-4e6b-b9a1-c49761d6f9ce")
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.RolePage", b =>
                {
                    b.HasOne("Calisan_Yonetim_Core.Models.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Calisan_Yonetim_Core.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.User", b =>
                {
                    b.HasOne("Calisan_Yonetim_Core.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Calisan_Yonetim_Core.Models.Personnel", "Personnel")
                        .WithOne("User")
                        .HasForeignKey("Calisan_Yonetim_Core.Models.User", "PersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Personnel");
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.UserRole", b =>
                {
                    b.HasOne("Calisan_Yonetim_Core.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Calisan_Yonetim_Core.Models.Role", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId1");

                    b.HasOne("Calisan_Yonetim_Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Company", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Personnel", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
