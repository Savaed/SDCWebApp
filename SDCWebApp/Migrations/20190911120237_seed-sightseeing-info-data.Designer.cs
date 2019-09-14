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
    [Migration("20190911120237_seed-sightseeing-info-data")]
    partial class seedsightseeinginfodata
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
                            Id = "915154c2-f9f0-4a05-8c36-97ef6f7bc32f",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 772, DateTimeKind.Utc).AddTicks(6647),
                            Date = new DateTime(2019, 9, 11, 14, 2, 36, 771, DateTimeKind.Local).AddTicks(2647),
                            Description = "Attempt to steal the Dead Man's Chest",
                            Type = "LogIn",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            User = "jacks"
                        },
                        new
                        {
                            Id = "1c3511ac-257c-4ec0-a4c0-15bedd94929a",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 772, DateTimeKind.Utc).AddTicks(8364),
                            Date = new DateTime(2019, 9, 11, 14, 2, 36, 772, DateTimeKind.Local).AddTicks(8354),
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
                            Id = "3079afac-c8ab-4cfb-a20d-28bf7bbb3117",
                            Author = "Jack Sparrow",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 772, DateTimeKind.Utc).AddTicks(9879),
                            Text = "BlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearlBlackPearl",
                            Title = "The bast pirate i'v every seen",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "fdbe22d2-0a10-4c1a-86ef-f61b3c203c92",
                            Author = "Hektor Barbossa",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(1277),
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
                            Id = "b03dd3ca-bf0a-48e2-8315-cb74c8d7c0b5",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 776, DateTimeKind.Utc).AddTicks(2299),
                            DateOfBirth = new DateTime(1996, 9, 11, 14, 2, 36, 776, DateTimeKind.Local).AddTicks(2309),
                            EmailAddress = "example@mail.com",
                            HasFamilyCard = false,
                            IsChild = false,
                            IsDisabled = false,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "38754d7c-8a07-42b5-8361-87b7ccc5964f",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 776, DateTimeKind.Utc).AddTicks(3676),
                            DateOfBirth = new DateTime(2015, 9, 11, 14, 2, 36, 776, DateTimeKind.Local).AddTicks(3691),
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
                            Id = "80d3a5ca-a8db-4eb0-aeff-b9417869bb82",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(3993),
                            Description = "Discount for groups",
                            DiscountValueInPercentage = 15,
                            GroupSizeForDiscount = 20,
                            Type = "ForGroup",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "57b30168-7962-4385-a159-ac600f202fae",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(5742),
                            Description = "Discount for people with Family Card",
                            DiscountValueInPercentage = 15,
                            Type = "ForFamily",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "282d8606-c99e-442c-bdb8-2b2034456aa6",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(5761),
                            Description = "Discount for disabled people.",
                            DiscountValueInPercentage = 50,
                            Type = "ForDisabled",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "5d9087d3-f88b-478d-8296-0cee76a6c129",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(5764),
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

                    b.ToTable("GeneralSightseeingInfo");

                    b.HasData(
                        new
                        {
                            Id = "5d12cac1-1e48-4be2-a778-04176e233411",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(9231),
                            Description = "TL;DR",
                            MaxAllowedGroupSize = 35,
                            MaxChildAge = 5,
                            MaxTicketOrderInterval = 4,
                            SightseeingDuration = 2f,
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
                            Id = "cb9450f0-e39d-4202-9ad1-bf4f5548acb1",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 770, DateTimeKind.Utc).AddTicks(7924),
                            DayOfWeek = "Monday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "4f5218e6-4694-4c69-88ca-da0d83b52e6f",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1359),
                            DayOfWeek = "Tuesday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "14faa5e2-dd27-43c4-9abc-7225af47334e",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1520),
                            DayOfWeek = "Wednesday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "cb12626c-5fc5-4f5d-b403-a068d7240537",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1540),
                            DayOfWeek = "Thursday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "b435acd2-5486-4e55-b1d6-663536e6e2ec",
                            ClosingHour = new TimeSpan(0, 18, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1566),
                            DayOfWeek = "Friday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "c107fa9b-8159-44cc-8509-c38ce00e3db3",
                            ClosingHour = new TimeSpan(0, 16, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1584),
                            DayOfWeek = "Saturday",
                            OpeningHour = new TimeSpan(0, 10, 0, 0, 0),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "4bc59dc6-d710-4400-b323-1443a10943f4",
                            ClosingHour = new TimeSpan(0, 16, 0, 0, 0),
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 771, DateTimeKind.Utc).AddTicks(1598),
                            DayOfWeek = "Sunday",
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
                            Id = "ee158de6-cc7d-41bd-837a-0939dbd1915e",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(7499),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 30,
                            SightseeingDate = new DateTime(2019, 9, 18, 12, 0, 0, 0, DateTimeKind.Local),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "02c31704-6d5e-4c6a-9cc5-31a228faa14c",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(8480),
                            CurrentGroupSize = 0,
                            IsAvailablePlace = true,
                            MaxGroupSize = 25,
                            SightseeingDate = new DateTime(2019, 9, 13, 16, 0, 0, 0, DateTimeKind.Local),
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
                            Id = "5454f1d0-24b4-46d0-84ec-ce58ccd049b1",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 775, DateTimeKind.Utc).AddTicks(6097),
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
                            Id = "9c1940cb-9108-4176-bba9-d8c5e82590cb",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(2265),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 11, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(2257),
                            TicketUniqueId = "d1ba6690-e4cf-45dd-a523-c0408df6bbab",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 9, 18, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(2723)
                        },
                        new
                        {
                            Id = "c8045805-6091-46fd-968c-e959d0d461ec",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(3205),
                            Price = 0f,
                            PurchaseDate = new DateTime(2019, 9, 11, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(3197),
                            TicketUniqueId = "88a0248d-f1da-4724-8df0-fc4ae1277e19",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidFor = new DateTime(2019, 10, 2, 14, 2, 36, 773, DateTimeKind.Local).AddTicks(3220)
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
                            Id = "3432266a-6957-46ca-a512-f2384792a031",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(6547),
                            DefaultPrice = 28f,
                            Description = "Centrum Dziedzictwa Szkła i Piwnice Przedprożne.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "e9b58082-80f2-46ff-8ed5-5a97a2f198dd",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(7403),
                            DefaultPrice = 22f,
                            Description = "Centrum Dziedzictwa Szkła.",
                            IsPerHour = false,
                            IsPerPerson = true,
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = "c666b507-9e37-4885-b459-750788e1889f",
                            CreatedAt = new DateTime(2019, 9, 11, 12, 2, 36, 773, DateTimeKind.Utc).AddTicks(7417),
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
                    b.HasOne("SDCWebApp.Models.GeneralSightseeingInfo", "Info")
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
                    b.HasOne("SDCWebApp.Models.SightseeingTariff", "SightseeingTariff")
                        .WithMany("TicketTariffs")
                        .HasForeignKey("SightseeingTariffId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}