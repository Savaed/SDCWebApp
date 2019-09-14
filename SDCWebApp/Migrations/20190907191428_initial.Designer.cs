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
    [Migration("20190907191428_initial")]
    partial class initial
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
                            Id = "fa7b3054-c94d-4b1c-a987-9b2d2c7abbc8",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 256, DateTimeKind.Utc).AddTicks(5430),
                            Date = new DateTime(2019, 9, 7, 21, 14, 28, 254, DateTimeKind.Local).AddTicks(8445),
                            Description = "Attempt to steal the Dead Man's Chest",
                            Type = "LogIn",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            User = "jacks"
                        },
                        new
                        {
                            Id = "ca6c3233-20bb-4d9b-86d7-898c502a3ff4",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 256, DateTimeKind.Utc).AddTicks(7969),
                            Date = new DateTime(2019, 9, 7, 21, 14, 28, 256, DateTimeKind.Local).AddTicks(7953),
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
                            Id = "e28e05bd-221b-4f53-9df0-d236f0403557",
                            Author = "Jack Sparrow",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(1767),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The bast pirate i'v every seen",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "c9f8aeff-cc94-4a7b-a8c2-55cb46d0363f",
                            Author = "Hektor Barbossa",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(3576),
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
                            Id = "47e39995-0b89-481b-a420-a648a22ea893",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 261, DateTimeKind.Utc).AddTicks(5744),
                            DateOfBirth = new DateTime(1996, 9, 7, 21, 14, 28, 261, DateTimeKind.Local).AddTicks(5755),
                            EmailAddress = "example@mail.com",
                            HasFamilyCard = false,
                            IsChild = false,
                            IsDisabled = false,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "7199faf7-7678-4979-8976-efee55418248",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 261, DateTimeKind.Utc).AddTicks(7608),
                            DateOfBirth = new DateTime(2015, 9, 7, 21, 14, 28, 261, DateTimeKind.Local).AddTicks(7618),
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
                            Id = "0f900b49-1482-472c-af36-add758689d4e",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(7493),
                            Description = "Discount for groups",
                            DiscountValueInPercentage = 15,
                            GroupSizeForDiscount = 20,
                            Type = "ForGroup",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "f87af06f-7949-4e63-a5cc-3cba058a18ad",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(9515),
                            Description = "Discount for people with Family Card",
                            DiscountValueInPercentage = 15,
                            Type = "ForFamily",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "740335e9-1b77-4399-b348-17c965b2cc11",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(9533),
                            Description = "Discount for disabled people.",
                            DiscountValueInPercentage = 50,
                            Type = "ForDisabled",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "8d533126-4609-47b5-885b-fcb3b85b296f",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(9537),
                            Description = "Discount only for kids under specific age.",
                            DiscountValueInPercentage = 100,
                            Type = "ForChild",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.VisitInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("ClosingHour");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("MaxAllowedGroupSize");

                    b.Property<int>("MaxChildAge");

                    b.Property<int>("MaxTicketOrderInterval");

                    b.Property<TimeSpan>("OpeningHour");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("VisitInfo");

                    b.HasData(
                        new
                        {
                            Id = "23ded832-4d2e-4972-b5d2-d3fa673703f1",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 261, DateTimeKind.Utc).AddTicks(809),
                            Description = "TL;DR",
                            MaxAllowedGroupSize = 35,
                            MaxChildAge = 5,
                            MaxTicketOrderInterval = 4,
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
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
                            Id = "31636c00-1c99-4d56-8ef0-9b3b48fc2a54",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 260, DateTimeKind.Utc).AddTicks(8698),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 30,
                            SightseeingDate = new DateTime(2019, 9, 14, 21, 14, 28, 260, DateTimeKind.Local).AddTicks(8711),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "be76f1e2-d99a-4f47-ac1c-39dd7ee7c396",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 260, DateTimeKind.Utc).AddTicks(9743),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 25,
                            SightseeingDate = new DateTime(2019, 9, 9, 21, 14, 28, 260, DateTimeKind.Local).AddTicks(9753),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.VisitTariff", b =>
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
                            Id = "10881837-df57-4498-9b88-8031997b2fca",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 260, DateTimeKind.Utc).AddTicks(6803),
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
                            Id = "b98ab531-3b23-4956-9704-535b7efb35a7",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(5124),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 7, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(5115),
                            TicketUniqueId = "387759b2-b8ca-4012-840a-7174efcef96a",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 9, 14, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(5725)
                        },
                        new
                        {
                            Id = "d484384e-7467-4e4d-b605-673f00ad7b31",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 257, DateTimeKind.Utc).AddTicks(6220),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 7, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(6213),
                            TicketUniqueId = "e1b844a8-6b7a-4c3e-83e2-4ce8c4fc85bd",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 9, 28, 21, 14, 28, 257, DateTimeKind.Local).AddTicks(6247)
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
                            Id = "c72506a3-7f22-49ba-951d-b82c525f26f6",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 258, DateTimeKind.Utc).AddTicks(739),
                            DefaultPrice = 28f,
                            Description = "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "3bcd9759-18c6-4009-92d1-532c905f65c4",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 258, DateTimeKind.Utc).AddTicks(1601),
                            DefaultPrice = 22f,
                            Description = "Centrum Dziedzictwa Szkła.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "13a90443-c641-46e4-aa9e-26eaa7371fbc",
                            CreatedAt = new DateTime(2019, 9, 7, 19, 14, 28, 258, DateTimeKind.Utc).AddTicks(1666),
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
                    b.HasOne("SDCWebApp.Models.VisitTariff", "VisitTariff")
                        .WithMany("TicketTariffs")
                        .HasForeignKey("SightseeingTariffId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
