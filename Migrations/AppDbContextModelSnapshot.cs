﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proje.Models;

#nullable disable

namespace Proje.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Proje.Models.Abone", b =>
                {
                    b.Property<int>("AboneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AboneId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AboneId");

                    b.ToTable("Aboneler");
                });

            modelBuilder.Entity("Proje.Models.AltKategori", b =>
                {
                    b.Property<int>("AltKategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AltKategoriId"));

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fotograf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.HasKey("AltKategoriId");

                    b.HasIndex("KategoriId");

                    b.ToTable("AltKategoriler");
                });

            modelBuilder.Entity("Proje.Models.AppRole", b =>
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

            modelBuilder.Entity("Proje.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
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

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VerificationCodeExpiration")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Proje.Models.Ilce", b =>
                {
                    b.Property<int>("IlceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IlceId"));

                    b.Property<string>("IlceAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SehirId")
                        .HasColumnType("int");

                    b.HasKey("IlceId");

                    b.HasIndex("SehirId");

                    b.ToTable("Ilceler");
                });

            modelBuilder.Entity("Proje.Models.Kategori", b =>
                {
                    b.Property<int>("KategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategoriId"));

                    b.Property<string>("Fotograf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategoriId");

                    b.ToTable("Kategoriler");
                });

            modelBuilder.Entity("Proje.Models.Mesaj", b =>
                {
                    b.Property<int>("MesajId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MesajId"));

                    b.Property<string>("AlanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GonderenId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Icerik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MusteriProfilId")
                        .HasColumnType("int");

                    b.Property<int?>("TeklifId")
                        .HasColumnType("int");

                    b.Property<int?>("UzmanProfilId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ZamanDamgasi")
                        .HasColumnType("datetime2");

                    b.HasKey("MesajId");

                    b.HasIndex("AlanId");

                    b.HasIndex("GonderenId");

                    b.HasIndex("MusteriProfilId");

                    b.HasIndex("TeklifId");

                    b.HasIndex("UzmanProfilId");

                    b.ToTable("Mesajlar");
                });

            modelBuilder.Entity("Proje.Models.MusteriProfil", b =>
                {
                    b.Property<int>("MusteriProfilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MusteriProfilId"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IlceId")
                        .HasColumnType("int");

                    b.Property<string>("KullaniciId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostaKodu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SehirId")
                        .HasColumnType("int");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MusteriProfilId");

                    b.HasIndex("IlceId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("SehirId");

                    b.ToTable("MusteriProfilleri");
                });

            modelBuilder.Entity("Proje.Models.Sehir", b =>
                {
                    b.Property<int>("SehirId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SehirId"));

                    b.Property<string>("SehirAdi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SehirId");

                    b.ToTable("Sehirler");
                });

            modelBuilder.Entity("Proje.Models.TamamlanmisIs", b =>
                {
                    b.Property<int>("TamamlanmisIsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TamamlanmisIsId"));

                    b.Property<int>("Derece")
                        .HasColumnType("int");

                    b.Property<int>("TeklifId")
                        .HasColumnType("int");

                    b.Property<int>("UzmanProfilId")
                        .HasColumnType("int");

                    b.Property<string>("Yorum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TamamlanmisIsId");

                    b.HasIndex("TeklifId");

                    b.HasIndex("UzmanProfilId");

                    b.ToTable("TamamlanmisIsler");
                });

            modelBuilder.Entity("Proje.Models.Teklif", b =>
                {
                    b.Property<int>("TeklifId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeklifId"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AltKategoriId")
                        .HasColumnType("int");

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Durum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fotograf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IlceId")
                        .HasColumnType("int");

                    b.Property<int>("MusteriId")
                        .HasColumnType("int");

                    b.Property<int?>("SehirId")
                        .HasColumnType("int");

                    b.Property<int?>("UzmanProfilId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ZamanDamgasi")
                        .HasColumnType("datetime2");

                    b.HasKey("TeklifId");

                    b.HasIndex("AltKategoriId");

                    b.HasIndex("IlceId");

                    b.HasIndex("MusteriId");

                    b.HasIndex("SehirId");

                    b.HasIndex("UzmanProfilId");

                    b.ToTable("Teklifler");
                });

            modelBuilder.Entity("Proje.Models.UzmanProfil", b =>
                {
                    b.Property<int>("UzmanProfilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UzmanProfilId"));

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AltKategoriId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IlceId")
                        .HasColumnType("int");

                    b.Property<int?>("IsBasarisi")
                        .HasColumnType("int");

                    b.Property<int?>("KategoriId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("KayıtTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("KullaniciId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SaatlikUcret")
                        .HasColumnType("int");

                    b.Property<int?>("SehirId")
                        .HasColumnType("int");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ToplamIsler")
                        .HasColumnType("int");

                    b.Property<int?>("ToplamSaatler")
                        .HasColumnType("int");

                    b.Property<int>("ToplamYildiz")
                        .HasColumnType("int");

                    b.Property<int>("YorumSayisi")
                        .HasColumnType("int");

                    b.HasKey("UzmanProfilId");

                    b.HasIndex("AltKategoriId");

                    b.HasIndex("IlceId");

                    b.HasIndex("KategoriId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("SehirId");

                    b.ToTable("UzmanProfilleri");
                });

            modelBuilder.Entity("Proje.Models.UzmanYorum", b =>
                {
                    b.Property<int>("UzmanYorumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UzmanYorumId"));

                    b.Property<int?>("Derece")
                        .HasColumnType("int");

                    b.Property<int>("MusteriId")
                        .HasColumnType("int");

                    b.Property<int>("UzmanProfilId")
                        .HasColumnType("int");

                    b.Property<string>("Yorum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("YorumTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("UzmanYorumId");

                    b.HasIndex("MusteriId");

                    b.HasIndex("UzmanProfilId");

                    b.ToTable("UzmanYorumlari");
                });

            modelBuilder.Entity("Proje.Models.İletisim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GonderilmeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mesaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OkunmaTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("İletisimMesajları");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Proje.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Proje.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Proje.Models.AltKategori", b =>
                {
                    b.HasOne("Proje.Models.Kategori", "Kategori")
                        .WithMany("AltKategoriler")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("Proje.Models.Ilce", b =>
                {
                    b.HasOne("Proje.Models.Sehir", "Sehir")
                        .WithMany("Ilceler")
                        .HasForeignKey("SehirId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sehir");
                });

            modelBuilder.Entity("Proje.Models.Mesaj", b =>
                {
                    b.HasOne("Proje.Models.AppUser", "Alan")
                        .WithMany("AlinanMesajlar")
                        .HasForeignKey("AlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Proje.Models.AppUser", "Gonderen")
                        .WithMany("GonderilenMesajlar")
                        .HasForeignKey("GonderenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Proje.Models.MusteriProfil", null)
                        .WithMany("Mesajlar")
                        .HasForeignKey("MusteriProfilId");

                    b.HasOne("Proje.Models.Teklif", "Teklif")
                        .WithMany("Mesajlar")
                        .HasForeignKey("TeklifId");

                    b.HasOne("Proje.Models.UzmanProfil", null)
                        .WithMany("Mesajlar")
                        .HasForeignKey("UzmanProfilId");

                    b.Navigation("Alan");

                    b.Navigation("Gonderen");

                    b.Navigation("Teklif");
                });

            modelBuilder.Entity("Proje.Models.MusteriProfil", b =>
                {
                    b.HasOne("Proje.Models.Ilce", "Ilce")
                        .WithMany()
                        .HasForeignKey("IlceId");

                    b.HasOne("Proje.Models.AppUser", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.Sehir", "Sehir")
                        .WithMany()
                        .HasForeignKey("SehirId");

                    b.Navigation("Ilce");

                    b.Navigation("Kullanici");

                    b.Navigation("Sehir");
                });

            modelBuilder.Entity("Proje.Models.TamamlanmisIs", b =>
                {
                    b.HasOne("Proje.Models.Teklif", "Teklif")
                        .WithMany()
                        .HasForeignKey("TeklifId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.UzmanProfil", "Uzman")
                        .WithMany()
                        .HasForeignKey("UzmanProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teklif");

                    b.Navigation("Uzman");
                });

            modelBuilder.Entity("Proje.Models.Teklif", b =>
                {
                    b.HasOne("Proje.Models.AltKategori", "AltKategori")
                        .WithMany()
                        .HasForeignKey("AltKategoriId");

                    b.HasOne("Proje.Models.Ilce", "Ilce")
                        .WithMany()
                        .HasForeignKey("IlceId");

                    b.HasOne("Proje.Models.MusteriProfil", "Musteri")
                        .WithMany("Teklifler")
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Proje.Models.Sehir", "Sehir")
                        .WithMany()
                        .HasForeignKey("SehirId");

                    b.HasOne("Proje.Models.UzmanProfil", "Uzman")
                        .WithMany("Teklifler")
                        .HasForeignKey("UzmanProfilId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AltKategori");

                    b.Navigation("Ilce");

                    b.Navigation("Musteri");

                    b.Navigation("Sehir");

                    b.Navigation("Uzman");
                });

            modelBuilder.Entity("Proje.Models.UzmanProfil", b =>
                {
                    b.HasOne("Proje.Models.AltKategori", "AltKategori")
                        .WithMany()
                        .HasForeignKey("AltKategoriId");

                    b.HasOne("Proje.Models.Ilce", "Ilce")
                        .WithMany()
                        .HasForeignKey("IlceId");

                    b.HasOne("Proje.Models.Kategori", "Kategori")
                        .WithMany()
                        .HasForeignKey("KategoriId");

                    b.HasOne("Proje.Models.AppUser", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.Sehir", "Sehir")
                        .WithMany()
                        .HasForeignKey("SehirId");

                    b.Navigation("AltKategori");

                    b.Navigation("Ilce");

                    b.Navigation("Kategori");

                    b.Navigation("Kullanici");

                    b.Navigation("Sehir");
                });

            modelBuilder.Entity("Proje.Models.UzmanYorum", b =>
                {
                    b.HasOne("Proje.Models.MusteriProfil", "Musteri")
                        .WithMany()
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Proje.Models.UzmanProfil", "Uzman")
                        .WithMany("UzmanYorumlari")
                        .HasForeignKey("UzmanProfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Musteri");

                    b.Navigation("Uzman");
                });

            modelBuilder.Entity("Proje.Models.AppUser", b =>
                {
                    b.Navigation("AlinanMesajlar");

                    b.Navigation("GonderilenMesajlar");
                });

            modelBuilder.Entity("Proje.Models.Kategori", b =>
                {
                    b.Navigation("AltKategoriler");
                });

            modelBuilder.Entity("Proje.Models.MusteriProfil", b =>
                {
                    b.Navigation("Mesajlar");

                    b.Navigation("Teklifler");
                });

            modelBuilder.Entity("Proje.Models.Sehir", b =>
                {
                    b.Navigation("Ilceler");
                });

            modelBuilder.Entity("Proje.Models.Teklif", b =>
                {
                    b.Navigation("Mesajlar");
                });

            modelBuilder.Entity("Proje.Models.UzmanProfil", b =>
                {
                    b.Navigation("Mesajlar");

                    b.Navigation("Teklifler");

                    b.Navigation("UzmanYorumlari");
                });
#pragma warning restore 612, 618
        }
    }
}
