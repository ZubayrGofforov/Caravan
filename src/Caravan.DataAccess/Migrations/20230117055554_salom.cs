using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caravan.DataAccess.Migrations
{
    public partial class salom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 17, 5, 55, 54, 582, DateTimeKind.Utc).AddTicks(8815), new DateTime(2023, 1, 17, 5, 55, 54, 582, DateTimeKind.Utc).AddTicks(8818) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 17, 5, 46, 42, 780, DateTimeKind.Utc).AddTicks(9741), new DateTime(2023, 1, 17, 5, 46, 42, 780, DateTimeKind.Utc).AddTicks(9743) });
        }
    }
}
