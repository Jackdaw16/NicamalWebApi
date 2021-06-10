﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NicamalWebApi.DbContexts;

namespace NicamalWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210604193054_update04062021")]
    partial class update04062021
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("NicamalWebApi.Models.Disappearance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<string>("LastSeen")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Province")
                        .HasColumnType("longtext");

                    b.Property<string>("TelephoneContact")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Disappearances");
                });

            modelBuilder.Entity("NicamalWebApi.Models.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("NicamalWebApi.Models.Provinces", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Provinces");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Álava"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Albacete"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Alicante"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Almería"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Asturias"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Ávila"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Badajoz"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Barcelona"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Burgos"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Cáceres"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Cádiz"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Cantabria"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Castellón"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Ciudad Real"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Córdoba"
                        },
                        new
                        {
                            Id = 16,
                            Name = "A Coruña"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Cuenca"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Girona"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Granada"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Guadalajara"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Gipuzkoa"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Huelva"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Huesca"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Baleares"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Jaén"
                        },
                        new
                        {
                            Id = 26,
                            Name = "León"
                        },
                        new
                        {
                            Id = 27,
                            Name = "Lleida"
                        },
                        new
                        {
                            Id = 28,
                            Name = "Lugo"
                        },
                        new
                        {
                            Id = 29,
                            Name = "Madrid"
                        },
                        new
                        {
                            Id = 30,
                            Name = "Málaga"
                        },
                        new
                        {
                            Id = 31,
                            Name = "Murcia"
                        },
                        new
                        {
                            Id = 32,
                            Name = "Navarra"
                        },
                        new
                        {
                            Id = 33,
                            Name = "Ourense"
                        },
                        new
                        {
                            Id = 34,
                            Name = "Palencia"
                        },
                        new
                        {
                            Id = 35,
                            Name = "Las Palmas"
                        },
                        new
                        {
                            Id = 36,
                            Name = "Pontevedra"
                        },
                        new
                        {
                            Id = 37,
                            Name = "La Rioja"
                        },
                        new
                        {
                            Id = 38,
                            Name = "Salamanca"
                        },
                        new
                        {
                            Id = 39,
                            Name = "Segovia"
                        },
                        new
                        {
                            Id = 40,
                            Name = "Sevilla"
                        },
                        new
                        {
                            Id = 41,
                            Name = "Soria"
                        },
                        new
                        {
                            Id = 42,
                            Name = "Tarragona"
                        },
                        new
                        {
                            Id = 43,
                            Name = "Santa Crus de Tenerife"
                        },
                        new
                        {
                            Id = 44,
                            Name = "Teruel"
                        },
                        new
                        {
                            Id = 45,
                            Name = "Toledo"
                        },
                        new
                        {
                            Id = 46,
                            Name = "Valencia"
                        },
                        new
                        {
                            Id = 47,
                            Name = "Valladolid"
                        },
                        new
                        {
                            Id = 48,
                            Name = "Vizcaya"
                        },
                        new
                        {
                            Id = 49,
                            Name = "Zamora"
                        },
                        new
                        {
                            Id = 50,
                            Name = "Zaragoza"
                        },
                        new
                        {
                            Id = 51,
                            Name = "Ceuta"
                        },
                        new
                        {
                            Id = 52,
                            Name = "Melilla"
                        });
                });

            modelBuilder.Entity("NicamalWebApi.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("History")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsUrgent")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Observations")
                        .HasColumnType("longtext");

                    b.Property<string>("Personality")
                        .HasColumnType("longtext");

                    b.Property<string>("Species")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("NicamalWebApi.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("PublicationId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<int>("ReportedUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId")
                        .IsUnique();

                    b.HasIndex("ReportedUserId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("NicamalWebApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsShelter")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Province")
                        .HasColumnType("longtext");

                    b.Property<string>("SurNames")
                        .HasColumnType("longtext");

                    b.Property<string>("TelephoneContact")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Verify")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NicamalWebApi.Models.Publication", b =>
                {
                    b.HasOne("NicamalWebApi.Models.User", "User")
                        .WithMany("Publications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NicamalWebApi.Models.Report", b =>
                {
                    b.HasOne("NicamalWebApi.Models.Publication", "Publication")
                        .WithOne("Report")
                        .HasForeignKey("NicamalWebApi.Models.Report", "PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NicamalWebApi.Models.User", "ReportedUser")
                        .WithOne("Reported")
                        .HasForeignKey("NicamalWebApi.Models.Report", "ReportedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NicamalWebApi.Models.User", "User")
                        .WithOne("Report")
                        .HasForeignKey("NicamalWebApi.Models.Report", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("ReportedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NicamalWebApi.Models.Publication", b =>
                {
                    b.Navigation("Report");
                });

            modelBuilder.Entity("NicamalWebApi.Models.User", b =>
                {
                    b.Navigation("Publications");

                    b.Navigation("Report");

                    b.Navigation("Reported");
                });
#pragma warning restore 612, 618
        }
    }
}
