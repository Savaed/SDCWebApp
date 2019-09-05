﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SDCWebApp.Data;

namespace SDCWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190905141545_add-expiryin-column-to-refresh-token")]
    partial class addexpiryincolumntorefreshtoken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SDCWebApp.Models.ActivityLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Description");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("ActivityLogs");

                    b.HasData(
                        new
                        {
                            Id = "3584f443-33fc-45bd-a0a5-d1b972c93f98",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 868, DateTimeKind.Utc).AddTicks(5071),
                            Date = new DateTime(2019, 9, 5, 16, 15, 44, 866, DateTimeKind.Local).AddTicks(8821),
                            Description = "Attempt to steal the Dead Man's Chest",
                            Type = "LogIn",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            User = "jacks"
                        },
                        new
                        {
                            Id = "d0399544-4e41-4070-991e-3107617e4524",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 868, DateTimeKind.Utc).AddTicks(7033),
                            Date = new DateTime(2019, 9, 5, 16, 15, 44, 868, DateTimeKind.Local).AddTicks(7019),
                            Description = "Revolting on a black pearl",
                            Type = "LogOut",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            User = "hektorb"
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Article", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = "27d8fa7a-7d5f-4dcb-a51c-9d49eddf7784",
                            Author = "Jack Sparrow",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 868, DateTimeKind.Utc).AddTicks(9998),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The bast pirate i'v every seen",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "04650c0c-7c5e-405b-9a50-c99d90808d09",
                            Author = "Hektor Barbossa",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(1302),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The captain of Black Pearl",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("EmailAddress");

                    b.Property<bool>("HasFamilyCard");

                    b.Property<bool>("IsChild");

                    b.Property<bool>("IsDisabled");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = "61fcf266-c009-4228-ad0b-2fd3c79595ba",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 872, DateTimeKind.Utc).AddTicks(1982),
                            DateOfBirth = new DateTime(1996, 9, 5, 16, 15, 44, 872, DateTimeKind.Local).AddTicks(1992),
                            EmailAddress = "example@mail.com",
                            HasFamilyCard = false,
                            IsChild = false,
                            IsDisabled = false,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "14727643-9141-4474-bbd8-da9bc77c736b",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 872, DateTimeKind.Utc).AddTicks(3314),
                            DateOfBirth = new DateTime(2015, 9, 5, 16, 15, 44, 872, DateTimeKind.Local).AddTicks(3323),
                            EmailAddress = " example2@mail.uk",
                            HasFamilyCard = true,
                            IsChild = false,
                            IsDisabled = false,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Discount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("DiscountValueInPercentage");

                    b.Property<int?>("GroupSizeForDiscount");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            Id = "9bb34393-981b-48cf-9686-52c19f674dd8",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(3992),
                            Description = "Discount for groups",
                            DiscountValueInPercentage = 15,
                            GroupSizeForDiscount = 20,
                            Type = "ForGroup",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "8ea64c3e-9030-4835-92fa-cdd373ba8711",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(5680),
                            Description = "Discount for people with Family Card",
                            DiscountValueInPercentage = 15,
                            Type = "ForFamily",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "b439511d-d239-479a-944b-306c3bd5c690",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(5698),
                            Description = "Discount for disabled people.",
                            DiscountValueInPercentage = 50,
                            Type = "ForDisabled",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "0ed93a76-da73-4b92-92ef-5cfd4ad73e54",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(5702),
                            Description = "Discount only for kids under specific age.",
                            DiscountValueInPercentage = 100,
                            Type = "ForChild",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.GeneralSightseeingInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("ClosingHour");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("MaxAllowedGroupSize");

                    b.Property<int>("MaxChildAge");

                    b.Property<float>("OpeningHour");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("GeneralSightseeingInfo");

                    b.HasData(
                        new
                        {
                            Id = "5cb885fe-dd67-4747-8669-87087aa23e68",
                            ClosingHour = 17f,
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(8711),
                            Description = "TL;DR",
                            MaxAllowedGroupSize = 35,
                            MaxChildAge = 5,
                            OpeningHour = 9f,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("ExpiryIn");

                    b.Property<string>("Token");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SDCWebApp.Models.SightseeingGroup", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CurrentGroupSize");

                    b.Property<bool>("IsAvailablePlace");

                    b.Property<int>("MaxGroupSize");

                    b.Property<DateTime>("SightseeingDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = "5cf29df4-7c37-4537-8182-20c47581dea8",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(6990),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 30,
                            SightseeingDate = new DateTime(2019, 9, 12, 16, 15, 44, 871, DateTimeKind.Local).AddTicks(7001),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "f75f9aa4-e2d4-4b2e-b2dc-bfcbfe72b2b0",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(7949),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 25,
                            SightseeingDate = new DateTime(2019, 9, 7, 16, 15, 44, 871, DateTimeKind.Local).AddTicks(7958),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.SightseeingTariff", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("SightseeingTariffs");

                    b.HasData(
                        new
                        {
                            Id = "3e973817-585e-4cfc-acba-2cd1fa812bc3",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 871, DateTimeKind.Utc).AddTicks(5650),
                            Name = "BasicTickets",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CustomerId");

                    b.Property<string>("DiscountId");

                    b.Property<string>("GroupId");

                    b.Property<float>("Price");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("TariffId");

                    b.Property<string>("TicketUniqueId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<DateTime>("ValidFor")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("GroupId");

                    b.HasIndex("TariffId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = "5d436cd2-d41e-4354-b576-8e0776f7bf95",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(2280),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 5, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(2272),
                            TicketUniqueId = "a236c022-d193-4d65-974f-8249b24e6feb",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 9, 12, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(2731)
                        },
                        new
                        {
                            Id = "97cf000f-4c03-4b86-a526-0ed9364968bb",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(3198),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 5, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(3191),
                            TicketUniqueId = "93271c21-90c8-4b8c-89e9-43377b1d71f7",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 9, 26, 16, 15, 44, 869, DateTimeKind.Local).AddTicks(3212)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.TicketTariff", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<float>("DefaultPrice");

                    b.Property<string>("Description");

                    b.Property<bool>("IsPerHour");

                    b.Property<bool>("IsPerPerson");

                    b.Property<string>("SightseeingTariffId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("SightseeingTariffId");

                    b.ToTable("TicketTariffs");

                    b.HasData(
                        new
                        {
                            Id = "a1e6d592-6b14-4d3b-b91a-d30645df1edd",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(6497),
                            DefaultPrice = 28f,
                            Description = "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "5d47ad2a-24b8-485a-9331-04b5a16cc181",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(7421),
                            DefaultPrice = 22f,
                            Description = "Centrum Dziedzictwa Szkła.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "c017b24e-2330-4cbe-95cf-20a2388b992d",
                            CreatedAt = new DateTime(2019, 9, 5, 14, 15, 44, 869, DateTimeKind.Utc).AddTicks(7442),
                            DefaultPrice = 10f,
                            Description = "Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SDCWebApp.Models.Ticket", b =>
                {
                    b.HasOne("SDCWebApp.Models.Customer", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId");

                    b.HasOne("SDCWebApp.Models.Discount", "Discount")
                        .WithMany("Tickets")
                        .HasForeignKey("DiscountId");

                    b.HasOne("SDCWebApp.Models.SightseeingGroup", "Group")
                        .WithMany("Tickets")
                        .HasForeignKey("GroupId");

                    b.HasOne("SDCWebApp.Models.TicketTariff", "Tariff")
                        .WithMany("Tickets")
                        .HasForeignKey("TariffId");
                });

            modelBuilder.Entity("SDCWebApp.Models.TicketTariff", b =>
                {
                    b.HasOne("SDCWebApp.Models.SightseeingTariff", "SightseeingTariff")
                        .WithMany("TicketTariffs")
                        .HasForeignKey("SightseeingTariffId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
