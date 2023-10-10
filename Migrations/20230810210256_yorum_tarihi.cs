using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Migrations
{
    /// <inheritdoc />
    public partial class yorum_tarihi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "YorumTarihi",
                table: "UzmanYorumlari",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YorumTarihi",
                table: "UzmanYorumlari");
        }
    }
}
