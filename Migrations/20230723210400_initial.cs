using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCodeExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fotograf = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Sehirler",
                columns: table => new
                {
                    SehirId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SehirAdi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehirler", x => x.SehirId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AltKategoriler",
                columns: table => new
                {
                    AltKategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fotograf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltKategoriler", x => x.AltKategoriId);
                    table.ForeignKey(
                        name: "FK_AltKategoriler_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    IlceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SehirId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_Ilceler_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusteriProfilleri",
                columns: table => new
                {
                    MusteriProfilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SehirId = table.Column<int>(type: "int", nullable: true),
                    IlceId = table.Column<int>(type: "int", nullable: true),
                    PostaKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriProfilleri", x => x.MusteriProfilId);
                    table.ForeignKey(
                        name: "FK_MusteriProfilleri_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusteriProfilleri_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId");
                    table.ForeignKey(
                        name: "FK_MusteriProfilleri_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId");
                });

            migrationBuilder.CreateTable(
                name: "UzmanProfilleri",
                columns: table => new
                {
                    UzmanProfilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaatlikUcret = table.Column<int>(type: "int", nullable: true),
                    SehirId = table.Column<int>(type: "int", nullable: true),
                    IlceId = table.Column<int>(type: "int", nullable: true),
                    IsBasarisi = table.Column<int>(type: "int", nullable: true),
                    ToplamIsler = table.Column<int>(type: "int", nullable: true),
                    ToplamSaatler = table.Column<int>(type: "int", nullable: true),
                    ToplamYildiz = table.Column<int>(type: "int", nullable: false),
                    YorumSayisi = table.Column<int>(type: "int", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: true),
                    AltKategoriId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzmanProfilleri", x => x.UzmanProfilId);
                    table.ForeignKey(
                        name: "FK_UzmanProfilleri_AltKategoriler_AltKategoriId",
                        column: x => x.AltKategoriId,
                        principalTable: "AltKategoriler",
                        principalColumn: "AltKategoriId");
                    table.ForeignKey(
                        name: "FK_UzmanProfilleri_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UzmanProfilleri_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId");
                    table.ForeignKey(
                        name: "FK_UzmanProfilleri_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriId");
                    table.ForeignKey(
                        name: "FK_UzmanProfilleri_Sehirler_SehirId",
                        column: x => x.SehirId,
                        principalTable: "Sehirler",
                        principalColumn: "SehirId");
                });

            migrationBuilder.CreateTable(
                name: "Teklifler",
                columns: table => new
                {
                    TeklifId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fotograf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    UzmanProfilId = table.Column<int>(type: "int", nullable: false),
                    AltKategoriId = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZamanDamgasi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teklifler", x => x.TeklifId);
                    table.ForeignKey(
                        name: "FK_Teklifler_AltKategoriler_AltKategoriId",
                        column: x => x.AltKategoriId,
                        principalTable: "AltKategoriler",
                        principalColumn: "AltKategoriId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teklifler_MusteriProfilleri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "MusteriProfilleri",
                        principalColumn: "MusteriProfilId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teklifler_UzmanProfilleri_UzmanProfilId",
                        column: x => x.UzmanProfilId,
                        principalTable: "UzmanProfilleri",
                        principalColumn: "UzmanProfilId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UzmanYorumlari",
                columns: table => new
                {
                    UzmanYorumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Yorum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Derece = table.Column<int>(type: "int", nullable: false),
                    UzmanProfilId = table.Column<int>(type: "int", nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzmanYorumlari", x => x.UzmanYorumId);
                    table.ForeignKey(
                        name: "FK_UzmanYorumlari_MusteriProfilleri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "MusteriProfilleri",
                        principalColumn: "MusteriProfilId");
                    table.ForeignKey(
                        name: "FK_UzmanYorumlari_UzmanProfilleri_UzmanProfilId",
                        column: x => x.UzmanProfilId,
                        principalTable: "UzmanProfilleri",
                        principalColumn: "UzmanProfilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mesajlar",
                columns: table => new
                {
                    MesajId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZamanDamgasi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GonderenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeklifId = table.Column<int>(type: "int", nullable: false),
                    MusteriProfilId = table.Column<int>(type: "int", nullable: true),
                    UzmanProfilId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesajlar", x => x.MesajId);
                    table.ForeignKey(
                        name: "FK_Mesajlar_AspNetUsers_AlanId",
                        column: x => x.AlanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mesajlar_AspNetUsers_GonderenId",
                        column: x => x.GonderenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mesajlar_MusteriProfilleri_MusteriProfilId",
                        column: x => x.MusteriProfilId,
                        principalTable: "MusteriProfilleri",
                        principalColumn: "MusteriProfilId");
                    table.ForeignKey(
                        name: "FK_Mesajlar_Teklifler_TeklifId",
                        column: x => x.TeklifId,
                        principalTable: "Teklifler",
                        principalColumn: "TeklifId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mesajlar_UzmanProfilleri_UzmanProfilId",
                        column: x => x.UzmanProfilId,
                        principalTable: "UzmanProfilleri",
                        principalColumn: "UzmanProfilId");
                });

            migrationBuilder.CreateTable(
                name: "TamamlanmisIsler",
                columns: table => new
                {
                    TamamlanmisIsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzmanProfilId = table.Column<int>(type: "int", nullable: false),
                    TeklifId = table.Column<int>(type: "int", nullable: false),
                    Derece = table.Column<int>(type: "int", nullable: false),
                    Yorum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamamlanmisIsler", x => x.TamamlanmisIsId);
                    table.ForeignKey(
                        name: "FK_TamamlanmisIsler_Teklifler_TeklifId",
                        column: x => x.TeklifId,
                        principalTable: "Teklifler",
                        principalColumn: "TeklifId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TamamlanmisIsler_UzmanProfilleri_UzmanProfilId",
                        column: x => x.UzmanProfilId,
                        principalTable: "UzmanProfilleri",
                        principalColumn: "UzmanProfilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AltKategoriler_KategoriId",
                table: "AltKategoriler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ilceler_SehirId",
                table: "Ilceler",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_AlanId",
                table: "Mesajlar",
                column: "AlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_GonderenId",
                table: "Mesajlar",
                column: "GonderenId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_MusteriProfilId",
                table: "Mesajlar",
                column: "MusteriProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_TeklifId",
                table: "Mesajlar",
                column: "TeklifId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesajlar_UzmanProfilId",
                table: "Mesajlar",
                column: "UzmanProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriProfilleri_IlceId",
                table: "MusteriProfilleri",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriProfilleri_KullaniciId",
                table: "MusteriProfilleri",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriProfilleri_SehirId",
                table: "MusteriProfilleri",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_TamamlanmisIsler_TeklifId",
                table: "TamamlanmisIsler",
                column: "TeklifId");

            migrationBuilder.CreateIndex(
                name: "IX_TamamlanmisIsler_UzmanProfilId",
                table: "TamamlanmisIsler",
                column: "UzmanProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Teklifler_AltKategoriId",
                table: "Teklifler",
                column: "AltKategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Teklifler_MusteriId",
                table: "Teklifler",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Teklifler_UzmanProfilId",
                table: "Teklifler",
                column: "UzmanProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_UzmanProfilleri_AltKategoriId",
                table: "UzmanProfilleri",
                column: "AltKategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_UzmanProfilleri_IlceId",
                table: "UzmanProfilleri",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_UzmanProfilleri_KategoriId",
                table: "UzmanProfilleri",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_UzmanProfilleri_KullaniciId",
                table: "UzmanProfilleri",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_UzmanProfilleri_SehirId",
                table: "UzmanProfilleri",
                column: "SehirId");

            migrationBuilder.CreateIndex(
                name: "IX_UzmanYorumlari_MusteriId",
                table: "UzmanYorumlari",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_UzmanYorumlari_UzmanProfilId",
                table: "UzmanYorumlari",
                column: "UzmanProfilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Mesajlar");

            migrationBuilder.DropTable(
                name: "TamamlanmisIsler");

            migrationBuilder.DropTable(
                name: "UzmanYorumlari");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Teklifler");

            migrationBuilder.DropTable(
                name: "MusteriProfilleri");

            migrationBuilder.DropTable(
                name: "UzmanProfilleri");

            migrationBuilder.DropTable(
                name: "AltKategoriler");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Sehirler");
        }
    }
}
