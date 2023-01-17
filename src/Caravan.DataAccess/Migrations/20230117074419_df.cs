using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caravan.DataAccess.Migrations
{
    public partial class df : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 17, 7, 44, 19, 54, DateTimeKind.Utc).AddTicks(6798), new DateTime(2023, 1, 17, 7, 44, 19, 54, DateTimeKind.Utc).AddTicks(6800) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 16, 16, 3, 42, 665, DateTimeKind.Utc).AddTicks(3721), new DateTime(2023, 1, 16, 16, 3, 42, 665, DateTimeKind.Utc).AddTicks(3722) });
        }
    }
}
