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
    [Migration("20221117124531_InitialMigration")]
    partial class InitialMigration
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
                    b.Property<Guid>("EstateOptionsEstateOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstatesEstateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EstateOptionsEstateOptionId", "EstatesEstateId");

                    b.HasIndex("EstatesEstateId");

                    b.ToTable("EstateEstateOption");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
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

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                            RoleId = new Guid("3d128fbb-2270-4b17-baf9-80a4eb7f9875")
                        });
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
                            PasswordHash = "AQAAAAEAACcQAAAAEE7NsE/hTCh0OO0sN6OGW4F3jNtHthaNB6eiDYrxNopi0RafnLu5s4n7pOGdLhhLtA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f1966768-cc24-46f3-a626-adedeb0eb974",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.BuildingPlan", b =>
                {
                    b.Property<Guid>("BuildingPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingPlanName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BuildingPlanId");

                    b.ToTable("BuildingPlans");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.BuildingType", b =>
                {
                    b.Property<Guid>("BuildingTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingTypeName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BuildingTypeId");

                    b.ToTable("BuildingTypes");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
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

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Estate", b =>
                {
                    b.Property<Guid>("EstateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("AgentUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Balconies")
                        .HasColumnType("int");

                    b.Property<int>("BathRooms")
                        .HasColumnType("int");

                    b.Property<Guid>("BuildingPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EstateConditionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EstateName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("KitchenArea")
                        .HasColumnType("float");

                    b.Property<double>("LivingArea")
                        .HasColumnType("float");

                    b.Property<Guid>("MapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ParkingSpaces")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rooms")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalArea")
                        .HasColumnType("float");

                    b.Property<Guid>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EstateId");

                    b.HasIndex("AgentUserId");

                    b.HasIndex("BuildingPlanId");

                    b.HasIndex("BuildingTypeId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EstateConditionId");

                    b.HasIndex("MapId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Estates");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.EstateCondition", b =>
                {
                    b.Property<Guid>("EstateConditionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EstateConditionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EstateConditionId");

                    b.ToTable("EstateConditions");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.EstateOption", b =>
                {
                    b.Property<Guid>("EstateOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EstateOptionName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EstateOptionId");

                    b.ToTable("EstateOptions");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Map", b =>
                {
                    b.Property<Guid>("MapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstateAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MapId");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Photo", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EstateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PhotoId");

                    b.HasIndex("EstateId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Zone", b =>
                {
                    b.Property<Guid>("ZoneId")
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

                    b.HasKey("ZoneId");

                    b.HasIndex("ParentZoneId");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("EstateEstateOption", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.EstateOption", null)
                        .WithMany()
                        .HasForeignKey("EstateOptionsEstateOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.Estate", null)
                        .WithMany()
                        .HasForeignKey("EstatesEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
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
                    b.HasOne("RealEstateAgency.Core.Entities.Category", null)
                        .WithMany("Categories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("RealEstateAgency.Core.Entities.Estate", b =>
                {
                    b.HasOne("RealEstateAgency.Core.Entities.AgentUser", "AgentUser")
                        .WithMany("Estates")
                        .HasForeignKey("AgentUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.BuildingPlan", "BuildingPlan")
                        .WithMany("Estates")
                        .HasForeignKey("BuildingPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.BuildingType", "BuildingType")
                        .WithMany("Estates")
                        .HasForeignKey("BuildingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.Category", "Category")
                        .WithMany("Estates")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.EstateCondition", "EstateCondition")
                        .WithMany("Estates")
                        .HasForeignKey("EstateConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateAgency.Core.Entities.Zone", "Zone")
                        .WithMany("Estates")
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AgentUser");

                    b.Navigation("BuildingPlan");

                    b.Navigation("BuildingType");

                    b.Navigation("Category");

                    b.Navigation("EstateCondition");

                    b.Navigation("Map");

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
                    b.HasOne("RealEstateAgency.Core.Entities.Zone", null)
                        .WithMany("Zones")
                        .HasForeignKey("ParentZoneId");
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
                    b.Navigation("Categories");

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

                    b.Navigation("Zones");
                });
#pragma warning restore 612, 618
        }
    }
}
