﻿// <auto-generated />
using System;
using ExpressVoitures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpressVoitures.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Annee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Valeur")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Annees");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Finition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Finitions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nom = "LE"
                        },
                        new
                        {
                            Id = 2,
                            Nom = "Sport"
                        },
                        new
                        {
                            Id = 3,
                            Nom = "TCe"
                        },
                        new
                        {
                            Id = 4,
                            Nom = "XLT"
                        },
                        new
                        {
                            Id = 5,
                            Nom = "LX"
                        },
                        new
                        {
                            Id = 6,
                            Nom = "S"
                        },
                        new
                        {
                            Id = 7,
                            Nom = "SEL"
                        });
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehiculeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculeId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Marque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marques");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nom = "Mazda"
                        },
                        new
                        {
                            Id = 2,
                            Nom = "Jeep"
                        },
                        new
                        {
                            Id = 3,
                            Nom = "Renault"
                        },
                        new
                        {
                            Id = 4,
                            Nom = "Ford"
                        },
                        new
                        {
                            Id = 5,
                            Nom = "Honda"
                        },
                        new
                        {
                            Id = 6,
                            Nom = "Volkswagen"
                        },
                        new
                        {
                            Id = 7,
                            Nom = "Peugeot"
                        },
                        new
                        {
                            Id = 8,
                            Nom = "Volvo"
                        });
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Modele", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MarqueId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MarqueId");

                    b.ToTable("Modeles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MarqueId = 1,
                            Nom = "Miata"
                        },
                        new
                        {
                            Id = 2,
                            MarqueId = 2,
                            Nom = "Liberty"
                        },
                        new
                        {
                            Id = 3,
                            MarqueId = 3,
                            Nom = "Scénic"
                        },
                        new
                        {
                            Id = 4,
                            MarqueId = 4,
                            Nom = "Explorer"
                        },
                        new
                        {
                            Id = 5,
                            MarqueId = 5,
                            Nom = "Civic"
                        },
                        new
                        {
                            Id = 6,
                            MarqueId = 6,
                            Nom = "GTI"
                        },
                        new
                        {
                            Id = 7,
                            MarqueId = 4,
                            Nom = "Edge"
                        },
                        new
                        {
                            Id = 8,
                            MarqueId = 7,
                            Nom = "3008"
                        },
                        new
                        {
                            Id = 9,
                            MarqueId = 7,
                            Nom = "206"
                        },
                        new
                        {
                            Id = 10,
                            MarqueId = 8,
                            Nom = "XC60"
                        },
                        new
                        {
                            Id = 11,
                            MarqueId = 8,
                            Nom = "XC90"
                        });
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Reparation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cout")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehiculeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculeId")
                        .IsUnique();

                    b.ToTable("Reparations");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Vehicule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnneeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAchat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateVente")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Disponible")
                        .HasColumnType("bit");

                    b.Property<int>("FinitionId")
                        .HasColumnType("int");

                    b.Property<int>("MarqueId")
                        .HasColumnType("int");

                    b.Property<int>("ModeleId")
                        .HasColumnType("int");

                    b.Property<double>("PrixAchat")
                        .HasColumnType("float");

                    b.Property<double>("PrixVente")
                        .HasColumnType("float");

                    b.Property<int>("ReparationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnneeId");

                    b.HasIndex("FinitionId");

                    b.HasIndex("MarqueId");

                    b.HasIndex("ModeleId");

                    b.ToTable("Vehicules");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

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

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Image", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Entities.Vehicule", "Vehicule")
                        .WithMany("Images")
                        .HasForeignKey("VehiculeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicule");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Modele", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Entities.Marque", "Marque")
                        .WithMany("Modeles")
                        .HasForeignKey("MarqueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Marque");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Reparation", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Entities.Vehicule", "Vehicule")
                        .WithOne("Reparation")
                        .HasForeignKey("ExpressVoitures.Models.Entities.Reparation", "VehiculeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicule");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Vehicule", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Entities.Annee", "Annee")
                        .WithMany("Vehicules")
                        .HasForeignKey("AnneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpressVoitures.Models.Entities.Finition", "Finition")
                        .WithMany("Vehicules")
                        .HasForeignKey("FinitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ExpressVoitures.Models.Entities.Marque", "Marque")
                        .WithMany("Vehicules")
                        .HasForeignKey("MarqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpressVoitures.Models.Entities.Modele", "Modele")
                        .WithMany("Vehicules")
                        .HasForeignKey("ModeleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Annee");

                    b.Navigation("Finition");

                    b.Navigation("Marque");

                    b.Navigation("Modele");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Annee", b =>
                {
                    b.Navigation("Vehicules");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Finition", b =>
                {
                    b.Navigation("Vehicules");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Marque", b =>
                {
                    b.Navigation("Modeles");

                    b.Navigation("Vehicules");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Modele", b =>
                {
                    b.Navigation("Vehicules");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Entities.Vehicule", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Reparation")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
