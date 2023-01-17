using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caravan.DataAccess.Migrations
{
    public partial class price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 17, 5, 29, 15, 144, DateTimeKind.Utc).AddTicks(3807), new DateTime(2023, 1, 17, 5, 29, 15, 144, DateTimeKind.Utc).AddTicks(3809) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 17, 5, 23, 11, 535, DateTimeKind.Utc).AddTicks(2733), new DateTime(2023, 1, 17, 5, 23, 11, 535, DateTimeKind.Utc).AddTicks(2735) });
        }
    }
}
