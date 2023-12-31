﻿// <auto-generated />
using System;
using AP.MyTreeFarm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AP.MyTreeFarm.Infrastructure.Migrations
{
    [DbContext(typeof(MyTreeFarmContext))]
    [Migration("20221223121816_removepass")]
    partial class removepass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Auth0Id")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("tblEmployees", "Employee");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Auth0Id = "auth0|63a21b713393043784814183",
                            Email = "redphorcys@hotmail.com",
                            EmployeeId = "W_123",
                            FirstName = "Bert",
                            IsActive = true,
                            IsAdmin = false,
                            LastName = "Bibber",
                            UserName = "BibberendeBert"
                        },
                        new
                        {
                            Id = 2,
                            Auth0Id = "auth0|63a21af0e2c4faec70d45c72",
                            Email = "nick-hellemans@hotmail.com",
                            EmployeeId = "W_124",
                            FirstName = "Bart",
                            IsActive = true,
                            IsAdmin = false,
                            LastName = "Bobber",
                            UserName = "BobberendeBart"
                        },
                        new
                        {
                            Id = 3,
                            Auth0Id = "auth0|63741b6967ad3e09030c7bfe",
                            Email = "s115990@ap.be",
                            EmployeeId = "A_1",
                            FirstName = "Nick",
                            IsActive = true,
                            IsAdmin = true,
                            LastName = "Hellemans",
                            UserName = "Hel_Nick"
                        },
                        new
                        {
                            Id = 4,
                            Auth0Id = "auth0|63a42ea844641e0a186ca430",
                            Email = "s117923@ap.be",
                            EmployeeId = "A_1",
                            FirstName = "Chad",
                            IsActive = true,
                            IsAdmin = false,
                            LastName = "Thunderglock",
                            UserName = "Chad"
                        });
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Site", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("MapPicturePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("tblSites", "Site");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MapPicturePath = "farm_map.png",
                            Name = "Site_Ellerman",
                            PostalCode = "2000",
                            Street = "Ellermanstraat",
                            StreetNumber = "61"
                        },
                        new
                        {
                            Id = 2,
                            MapPicturePath = "farm_map.png",
                            Name = "Site_Meir",
                            PostalCode = "2000",
                            Street = "Lange Nieuwstraat",
                            StreetNumber = "35"
                        });
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Tree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("InstructionsUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("QrCodeUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("tblTrees", "Tree");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InstructionsUrl = "appelboom.pdf",
                            Name = "Malus sylvestris",
                            PictureUrl = "appelboom.jpg",
                            QrCodeUrl = "appelboomQR.png"
                        },
                        new
                        {
                            Id = 2,
                            InstructionsUrl = "pruimenboom.pdf",
                            Name = "Reine Claude d'Oullins",
                            PictureUrl = "pruimenboom.jpg",
                            QrCodeUrl = "pruimenboomQR.png"
                        });
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.TreeTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DatePaused")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DatePlanned")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TimePaused")
                        .HasColumnType("int");

                    b.Property<int>("ZoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ZoneId");

                    b.ToTable("tblTreeTasks", "TreeTask");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5611),
                            DateEnd = new DateTime(2022, 12, 24, 14, 18, 16, 2, DateTimeKind.Local).AddTicks(5657),
                            DatePlanned = new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5661),
                            DateStart = new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5652),
                            Description = "Uitleg taak1",
                            Duration = 50,
                            EmployeeId = 1,
                            Name = "Taak1",
                            Priority = 1,
                            Status = 3,
                            TimePaused = 0,
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5664),
                            DateEnd = new DateTime(2022, 12, 24, 15, 23, 16, 2, DateTimeKind.Local).AddTicks(5668),
                            DatePlanned = new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5670),
                            DateStart = new DateTime(2022, 12, 24, 14, 23, 16, 2, DateTimeKind.Local).AddTicks(5666),
                            Description = "Uitleg taak2",
                            Duration = 60,
                            EmployeeId = 1,
                            Name = "Taak2",
                            Priority = 2,
                            Status = 3,
                            TimePaused = 0,
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5672),
                            DateEnd = new DateTime(2022, 12, 24, 17, 18, 16, 2, DateTimeKind.Local).AddTicks(5676),
                            DatePaused = new DateTime(2022, 12, 24, 16, 18, 16, 2, DateTimeKind.Local).AddTicks(5680),
                            DatePlanned = new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5678),
                            DateStart = new DateTime(2022, 12, 24, 15, 33, 16, 2, DateTimeKind.Local).AddTicks(5674),
                            Description = "Uitleg taak3",
                            Duration = 60,
                            EmployeeId = 1,
                            Name = "Taak3",
                            Priority = 2,
                            Status = 0,
                            TimePaused = 45,
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5682),
                            DateEnd = new DateTime(2022, 12, 24, 19, 23, 16, 2, DateTimeKind.Local).AddTicks(5685),
                            DatePlanned = new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5687),
                            DateStart = new DateTime(2022, 12, 24, 17, 23, 16, 2, DateTimeKind.Local).AddTicks(5683),
                            Description = "Uitleg taak4",
                            Duration = 120,
                            EmployeeId = 1,
                            Name = "Taak4",
                            Priority = 3,
                            Status = 3,
                            TimePaused = 0,
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5689),
                            DateEnd = new DateTime(2022, 12, 25, 15, 18, 16, 2, DateTimeKind.Local).AddTicks(5692),
                            DatePlanned = new DateTime(2022, 12, 25, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5694),
                            DateStart = new DateTime(2022, 12, 25, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5691),
                            Description = "Uitleg taak5",
                            Duration = 120,
                            EmployeeId = 1,
                            Name = "Taak5",
                            Priority = 1,
                            Status = 3,
                            TimePaused = 0,
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5697),
                            DateEnd = new DateTime(2022, 12, 24, 15, 18, 16, 2, DateTimeKind.Local).AddTicks(5700),
                            DatePlanned = new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            DateStart = new DateTime(2022, 12, 24, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5698),
                            Description = "Uitleg taak6",
                            Duration = 120,
                            EmployeeId = 2,
                            Name = "Taak6",
                            Priority = 1,
                            Status = 3,
                            TimePaused = 0,
                            ZoneId = 3
                        },
                        new
                        {
                            Id = 7,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5705),
                            DateEnd = new DateTime(2022, 12, 24, 18, 8, 16, 2, DateTimeKind.Local).AddTicks(5708),
                            DatePaused = new DateTime(2022, 12, 24, 16, 23, 16, 2, DateTimeKind.Local).AddTicks(5712),
                            DatePlanned = new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            DateStart = new DateTime(2022, 12, 24, 15, 23, 16, 2, DateTimeKind.Local).AddTicks(5707),
                            Description = "Uitleg taak7",
                            Duration = 120,
                            EmployeeId = 2,
                            Name = "Taak7",
                            Priority = 2,
                            Status = 3,
                            TimePaused = 45,
                            ZoneId = 3
                        },
                        new
                        {
                            Id = 8,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5715),
                            DateEnd = new DateTime(2022, 12, 24, 19, 18, 16, 2, DateTimeKind.Local).AddTicks(5718),
                            DatePlanned = new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            DateStart = new DateTime(2022, 12, 24, 18, 18, 16, 2, DateTimeKind.Local).AddTicks(5716),
                            Description = "Uitleg taak8",
                            Duration = 60,
                            EmployeeId = 2,
                            Name = "Taak8",
                            Priority = 3,
                            Status = 3,
                            TimePaused = 0,
                            ZoneId = 4
                        },
                        new
                        {
                            Id = 9,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5722),
                            DateEnd = new DateTime(2022, 12, 24, 20, 3, 16, 2, DateTimeKind.Local).AddTicks(5726),
                            DatePaused = new DateTime(2022, 12, 24, 19, 43, 16, 2, DateTimeKind.Local).AddTicks(5729),
                            DatePlanned = new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            DateStart = new DateTime(2022, 12, 24, 19, 23, 16, 2, DateTimeKind.Local).AddTicks(5724),
                            Description = "Uitleg taak9",
                            Duration = 1,
                            EmployeeId = 2,
                            Name = "Taak9",
                            Priority = 3,
                            Status = 0,
                            TimePaused = 10,
                            ZoneId = 4
                        },
                        new
                        {
                            Id = 10,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5731),
                            DatePlanned = new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                            Duration = 120,
                            EmployeeId = 4,
                            Name = "Takken snoeien",
                            Priority = 1,
                            Status = 0,
                            TimePaused = 0,
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 11,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5736),
                            DatePlanned = new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                            Duration = 60,
                            EmployeeId = 4,
                            Name = "Gezondheidscheck",
                            Priority = 2,
                            Status = 0,
                            TimePaused = 0,
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 12,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5739),
                            DatePlanned = new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Verwijderen van rotte appels, appels met wormen/rupsen, ... ",
                            Duration = 60,
                            EmployeeId = 4,
                            Name = "Appelinspectie",
                            Priority = 2,
                            Status = 0,
                            TimePaused = 0,
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 13,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5743),
                            DatePlanned = new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                            Duration = 120,
                            EmployeeId = 4,
                            Name = "Onkruidverdelging",
                            Priority = 0,
                            Status = 0,
                            TimePaused = 0,
                            ZoneId = 1
                        },
                        new
                        {
                            Id = 14,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5747),
                            DatePlanned = new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                            Duration = 300,
                            EmployeeId = 4,
                            Name = "Morgen 1",
                            Priority = 1,
                            Status = 0,
                            TimePaused = 0,
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 15,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5751),
                            DatePlanned = new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                            Duration = 150,
                            EmployeeId = 4,
                            Name = "Morgen 2",
                            Priority = 0,
                            Status = 0,
                            TimePaused = 0,
                            ZoneId = 2
                        },
                        new
                        {
                            Id = 16,
                            DateCreated = new DateTime(2022, 12, 23, 13, 18, 16, 2, DateTimeKind.Local).AddTicks(5755),
                            DatePlanned = new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lacinia nisi ut urna ullamcorper, quis gravida ex venenatis. Sed elementum maximus eros at dignissim. Pellentesque vitae mi mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
                            Duration = 30,
                            EmployeeId = 4,
                            Name = "Morgen 3",
                            Priority = 0,
                            Status = 0,
                            TimePaused = 0,
                            ZoneId = 2
                        });
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Zone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SiteId")
                        .HasColumnType("int");

                    b.Property<double>("SurfaceArea")
                        .HasMaxLength(255)
                        .HasColumnType("float");

                    b.Property<int>("TreeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SiteId");

                    b.HasIndex("TreeId");

                    b.ToTable("tblZones", "Zone");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Eller_Zone1",
                            SiteId = 1,
                            SurfaceArea = 0.5,
                            TreeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Eller_Zone2",
                            SiteId = 1,
                            SurfaceArea = 0.5,
                            TreeId = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Meir_Zone1",
                            SiteId = 2,
                            SurfaceArea = 0.25,
                            TreeId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "Meir_Zone2",
                            SiteId = 2,
                            SurfaceArea = 0.25,
                            TreeId = 2
                        });
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.TreeTask", b =>
                {
                    b.HasOne("AP.MyTreeFarm.Domain.Employee", "Employee")
                        .WithMany("Tasks")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AP.MyTreeFarm.Domain.Zone", "Zone")
                        .WithMany("Tasks")
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Zone", b =>
                {
                    b.HasOne("AP.MyTreeFarm.Domain.Site", "Site")
                        .WithMany("Zones")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AP.MyTreeFarm.Domain.Tree", "Tree")
                        .WithMany("Zones")
                        .HasForeignKey("TreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");

                    b.Navigation("Tree");
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Employee", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Site", b =>
                {
                    b.Navigation("Zones");
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Tree", b =>
                {
                    b.Navigation("Zones");
                });

            modelBuilder.Entity("AP.MyTreeFarm.Domain.Zone", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
