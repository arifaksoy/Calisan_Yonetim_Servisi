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
    [Migration("20250102220033_test_migration7")]
    partial class test_migration7
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
                            CompanyId = new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"),
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
                            PageId = new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"),
                            PageDescription = "",
                            PageName = "Company",
                            Status = 1
                        },
                        new
                        {
                            PageId = new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"),
                            PageDescription = "",
                            PageName = "Pages",
                            Status = 1
                        });
                });

            modelBuilder.Entity("Calisan_Yonetim_Core.Models.Page2", b =>
                {
                    b.Property<Guid>("PageId2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PageDescription2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageName2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status2")
                        .HasColumnType("int");

                    b.HasKey("PageId2");

                    b.ToTable("Pages2");
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
                            PersonnelId = new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"),
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
                            RoleId = new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"),
                            RoleName = "System Admin",
                            Status = 1
                        },
                        new
                        {
                            RoleId = new Guid("80fd5f51-b495-4ed0-9bba-8af1fbd23372"),
                            RoleName = "Admin",
                            Status = 1
                        },
                        new
                        {
                            RoleId = new Guid("83e8d543-4230-4486-9071-34bead58ef65"),
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
                            RolePageId = new Guid("aba70ed3-7ce8-4b4d-9b65-6e5f40a70ac8"),
                            PageId = new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"),
                            RoleId = new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"),
                            Status = 1
                        },
                        new
                        {
                            RolePageId = new Guid("100ab2a1-f004-40f0-8e84-ff5e8f503ddd"),
                            PageId = new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"),
                            RoleId = new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"),
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
                            Id = new Guid("389a0451-994c-46e1-bb04-041c6bc90c44"),
                            CompanyId = new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"),
                            Password = "admin123",
                            PersonnelId = new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"),
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
                            UserRoleId = new Guid("250e8c6f-d742-41a4-ac87-b6c9a66c84d0"),
                            RoleId = new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"),
                            Status = 1,
                            UserId = new Guid("389a0451-994c-46e1-bb04-041c6bc90c44")
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
