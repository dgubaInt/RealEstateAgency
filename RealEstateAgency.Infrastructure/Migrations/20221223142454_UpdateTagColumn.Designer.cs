﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstateAgency.Infrastructure.Data;

#nullable disable

namespace RealEstateAgency.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221223142454_UpdateTagColumn")]
    partial class UpdateTagColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EstateEstateOption", b =>
                {
                    b.Property<Guid>("EstateOptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstatesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EstateOptionsId", "EstatesId");

                    b.HasIndex("EstatesId");

                    b.ToTable("EstateEstateOption");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.AgentUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4f743e54-0a01-46e1-a1bf-6f013f406211",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "admin",
                            LastName = "admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEAV9LMmte1SW+Yez3YudqEuw/xGvOLjgI/eH9p4zUb0NkzhJhqazGhc2NxWPoG6W+A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8a39600e-ec14-4e76-9af9-c948f45be380",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.BuildingPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingPlanName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BuildingPlans");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.BuildingType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingTypeName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BuildingTypes");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("535efb21-3c39-4cae-b302-5a176cfdd9d6"),
                            CategoryName = "Rent",
                            CreatedDate = new DateTime(2022, 12, 23, 16, 24, 53, 494, DateTimeKind.Local).AddTicks(3792),
                            Position = 0
                        },
                        new
                        {
                            Id = new Guid("5bfdfd10-6796-4653-a614-18e4dc1fac20"),
                            CategoryName = "Buy",
                            CreatedDate = new DateTime(2022, 12, 23, 16, 24, 53, 494, DateTimeKind.Local).AddTicks(3834),
                            Position = 0
                        });
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Estate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("AgentUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Balconies")
                        .HasColumnType("int");

                    b.Property<int>("BathRooms")
                        .HasColumnType("int");

                    b.Property<Guid?>("BuildingPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BuildingTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EstateConditionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EstateName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("KitchenArea")
                        .HasColumnType("float");

                    b.Property<double>("LivingArea")
                        .HasColumnType("float");

                    b.Property<int>("ParkingSpaces")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rooms")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalArea")
                        .HasColumnType("float");

                    b.Property<Guid?>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AgentUserId");

                    b.HasIndex("BuildingPlanId");

                    b.HasIndex("BuildingTypeId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EstateConditionId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Estates");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.EstateCondition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EstateConditionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("EstateConditions");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.EstateOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EstateOptionName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("EstateOptions");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EstateId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3d128fbb-2270-4b17-baf9-80a4eb7f9875"),
                            ConcurrencyStamp = "a4c871a1-73cf-428f-90f7-a273ccd4740c",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("c368d1b0-d46a-4b51-b737-c016faa91e31"),
                            ConcurrencyStamp = "f67a998b-7268-4e31-ae32-370aaaf10efc",
                            Name = "user",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Zone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ZoneName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("ParentZoneId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.UserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasDiscriminator().HasValue("UserRole");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                            RoleId = new Guid("3d128fbb-2270-4b17-baf9-80a4eb7f9875"),
                            Id = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("EstateEstateOption", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.EstateOption", null)
                        .WithMany()
                        .HasForeignKey("EstateOptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.Estate", null)
                        .WithMany()
                        .HasForeignKey("EstatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.AgentUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.AgentUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.AgentUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.AgentUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Category", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Estate", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.AgentUser", "AgentUser")
                        .WithMany("Estates")
                        .HasForeignKey("AgentUserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RealEstateAgency.Core.Entities.BuildingPlan", "BuildingPlan")
                        .WithMany("Estates")
                        .HasForeignKey("BuildingPlanId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RealEstateAgency.Core.Entities.BuildingType", "BuildingType")
                        .WithMany("Estates")
                        .HasForeignKey("BuildingTypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RealEstateAgency.Core.Entities.Category", "Category")
                        .WithMany("Estates")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RealEstateAgency.Core.Entities.EstateCondition", "EstateCondition")
                        .WithMany("Estates")
                        .HasForeignKey("EstateConditionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RealEstateAgency.Core.Entities.Zone", "Zone")
                        .WithMany("Estates")
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AgentUser");

                    b.Navigation("BuildingPlan");

                    b.Navigation("BuildingType");

                    b.Navigation("Category");

                    b.Navigation("EstateCondition");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Photo", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.Estate", "Estate")
                        .WithMany("Photos")
                        .HasForeignKey("EstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estate");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Zone", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.Zone", "ParentZone")
                        .WithMany()
                        .HasForeignKey("ParentZoneId");

                    b.Navigation("ParentZone");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.AgentUser", b =>
                {
                    b.Navigation("Estates");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.BuildingPlan", b =>
                {
                    b.Navigation("Estates");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.BuildingType", b =>
                {
                    b.Navigation("Estates");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Category", b =>
                {
                    b.Navigation("Estates");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Estate", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.EstateCondition", b =>
                {
                    b.Navigation("Estates");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Zone", b =>
                {
                    b.Navigation("Estates");
                });
#pragma warning restore 612, 618
        }
    }
}
