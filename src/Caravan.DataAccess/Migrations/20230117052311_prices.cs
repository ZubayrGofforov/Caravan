using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caravan.DataAccess.Migrations
{
    public partial class prices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 17, 5, 23, 11, 535, DateTimeKind.Utc).AddTicks(2733), new DateTime(2023, 1, 17, 5, 23, 11, 535, DateTimeKind.Utc).AddTicks(2735) });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryLocationId",
                table: "Orders",
                column: "DeliveryLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Locations_DeliveryLocationId",
                table: "Orders",
                column: "DeliveryLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Locations_DeliveryLocationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryLocationId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 1, 7, 19, 35, 22, 807, DateTimeKind.Utc).AddTicks(8328), new DateTime(2023, 1, 7, 19, 35, 22, 807, DateTimeKind.Utc).AddTicks(8330) });
        }
    }
}
