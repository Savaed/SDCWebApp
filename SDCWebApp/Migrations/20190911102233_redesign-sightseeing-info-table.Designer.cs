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
    [Migration("20190911102233_redesign-sightseeing-info-table")]
    partial class redesignsightseeinginfotable
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
                            Id = "94fef728-1750-41f9-8605-4e8b66d8b0e7",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 777, DateTimeKind.Utc).AddTicks(9526),
                            Date = new DateTime(2019, 9, 11, 12, 22, 32, 776, DateTimeKind.Local).AddTicks(5531),
                            Description = "Attempt to steal the Dead Man's Chest",
                            Type = "LogIn",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            User = "jacks"
                        },
                        new
                        {
                            Id = "e067927c-f7df-4dd8-aa67-d35bd258b591",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(1272),
                            Date = new DateTime(2019, 9, 11, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(1263),
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
                            Id = "551956a8-567c-44d1-a23c-e42604a01cab",
                            Author = "Jack Sparrow",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(3211),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The bast pirate i'v every seen",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "10826dff-1c1e-4912-9639-36cccb518df6",
                            Author = "Hektor Barbossa",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(4661),
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
                            Id = "96085bd5-44d3-4418-88b6-89e2b88d4233",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(6064),
                            DateOfBirth = new DateTime(1996, 9, 11, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(6074),
                            EmailAddress = "example@mail.com",
                            HasFamilyCard = false,
                            IsChild = false,
                            IsDisabled = false,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "70a909a9-8c64-4825-a476-e927a1a231b5",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(7420),
                            DateOfBirth = new DateTime(2015, 9, 11, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(7430),
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
                            Id = "f3911046-0b36-4cd6-a507-455e58ebcdb8",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(7309),
                            Description = "Discount for groups",
                            DiscountValueInPercentage = 15,
                            GroupSizeForDiscount = 20,
                            Type = "ForGroup",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "0ce45b1d-1823-44be-a9c2-9ce809cc6f5b",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9040),
                            Description = "Discount for people with Family Card",
                            DiscountValueInPercentage = 15,
                            Type = "ForFamily",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "1b649fe0-40cf-43a0-90da-059e4f553a03",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9057),
                            Description = "Discount for disabled people.",
                            DiscountValueInPercentage = 50,
                            Type = "ForDisabled",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "96db8a74-22e7-4ea3-90c0-5977de9d6b4a",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9060),
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

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("MaxAllowedGroupSize");

                    b.Property<int>("MaxChildAge");

                    b.Property<int>("MaxTicketOrderInterval");

                    b.Property<float>("SightseeingDuration");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("VisitInfo");

                    b.HasData(
                        new
                        {
                            Id = "9b2b0143-df2d-408b-af1c-f61540b6d942",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(3440),
                            Description = "TL;DR",
                            MaxAllowedGroupSize = 35,
                            MaxChildAge = 5,
                            MaxTicketOrderInterval = 4,
                            SightseeingDuration = 0f,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SDCWebApp.Models.OpeningHours", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("ClosingHour");

                    b.Property<byte[]>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("DayOfWeek")
                        .IsRequired();

                    b.Property<string>("InfoId");

                    b.Property<TimeSpan>("OpeningHour");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("InfoId");

                    b.ToTable("OpeningDates");

                    b.HasData(
                        new
                        {
                            Id = "8a39810a-05ea-4d7a-b190-5a2ddb0b7d42",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 776, DateTimeKind.Utc).AddTicks(608),
                            DayOfWeek = "Sunday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "a2aece52-66c1-441d-ab99-ac0acb491fa8",
                            ClosingHour = new TimeSpan(0, 10, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 776, DateTimeKind.Utc).AddTicks(4093),
                            DayOfWeek = "Saturday",
                            OpeningHour = new TimeSpan(0, 16, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "b3ec0dcd-2b97-4468-838a-315eff942d75",
                            ClosingHour = new TimeSpan(0, 10, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 776, DateTimeKind.Utc).AddTicks(4843),
                            DayOfWeek = "Monday",
                            OpeningHour = new TimeSpan(0, 18, 0, 0, 0),
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
                            Id = "e7aaa447-eb16-402b-ad1d-52debabae0ce",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(1040),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 30,
                            SightseeingDate = new DateTime(2019, 9, 18, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(1056),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "b17082e3-5878-4a16-957c-b5ada035dd67",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 781, DateTimeKind.Utc).AddTicks(2375),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 25,
                            SightseeingDate = new DateTime(2019, 9, 13, 12, 22, 32, 781, DateTimeKind.Local).AddTicks(2385),
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
                            Id = "c37359d1-b232-4a21-b3f2-106690109a5a",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 780, DateTimeKind.Utc).AddTicks(9632),
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
                            Id = "2da03878-b7e7-49e7-a111-c1275384c454",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(5586),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 11, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(5578),
                            TicketUniqueId = "c278362d-c0fc-4ba8-bfb9-d6fc33f40072",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 9, 18, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(6036)
                        },
                        new
                        {
                            Id = "4bd9cd2e-e48f-431e-bf92-03ccdd513c9a",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(6516),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 11, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(6510),
                            TicketUniqueId = "e6b8755c-49e2-4faf-9e14-2f7987df2c68",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 10, 2, 12, 22, 32, 778, DateTimeKind.Local).AddTicks(6531)
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
                            Id = "c8ab0404-b65a-4019-b4aa-63066e4f6da0",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 778, DateTimeKind.Utc).AddTicks(9845),
                            DefaultPrice = 28f,
                            Description = "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "9b8d4f66-fde8-41a9-8778-0c33e7f53f01",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 779, DateTimeKind.Utc).AddTicks(687),
                            DefaultPrice = 22f,
                            Description = "Centrum Dziedzictwa Szkła.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "a3b6ac26-36fc-4fcf-b1bf-d6489b0dcc5b",
                            CreatedAt = new DateTime(2019, 9, 11, 10, 22, 32, 779, DateTimeKind.Utc).AddTicks(702),
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

            modelBuilder.Entity("SDCWebApp.Models.OpeningHours", b =>
                {
                    b.HasOne("SDCWebApp.Models.VisitInfo", "Info")
                        .WithMany("OpeningHours")
                        .HasForeignKey("InfoId");
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
