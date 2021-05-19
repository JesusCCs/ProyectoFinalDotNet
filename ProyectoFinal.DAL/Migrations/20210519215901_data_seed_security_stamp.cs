using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class data_seed_security_stamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2a01e75a-8b8c-4f8b-b221-ae90074a776f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("6192df98-7aad-41db-8501-056cde66ea9b"));

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2832b280-1056-4413-b4d4-1e56f7265d6d", "AQAAAAEAACcQAAAAEBqjIjAJCyDukZzz4rqyw2WqRUapPQa1RP2oJQDvvpjeuEBBra7KdK5/9j5JOvG4Ng==", "21434fa0-d860-49b9-ac27-5d3fdb2018f2" });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "520e2a95-1cbc-4030-90c1-3324d9ad0c05");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2141cdf3-b3f0-40e7-9c9d-80d82fdc4cc1"), "f604a63a-dd8d-4778-ad7f-c90fe5de43b7", "Gimnasio", "GIMNASIO" },
                    { new Guid("628dd1f1-bdfb-4a74-bcde-0a3031e74407"), "a1f6fd41-17e5-4e22-b197-1f3f34bb4d57", "Usuario", "USUARIO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2141cdf3-b3f0-40e7-9c9d-80d82fdc4cc1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("628dd1f1-bdfb-4a74-bcde-0a3031e74407"));

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6d121f7-e67d-4e1c-bbfc-c8194e3e581c", "AQAAAAEAACcQAAAAEEQqDxaZ/rV+g/vMlSK7zAfor84LsTsRmL0TDOBZn41iakEfJlMLKvRLwk8sMP1LRw==", null });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "01cd6466-5318-4e3a-9270-eddc5370154f");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6192df98-7aad-41db-8501-056cde66ea9b"), "4b0005bb-c6e0-4e95-8595-0d4abfbbc9b6", "Gimnasio", "GIMNASIO" },
                    { new Guid("2a01e75a-8b8c-4f8b-b221-ae90074a776f"), "4a644594-8967-4c21-8c63-763cb6943074", "Usuario", "USUARIO" }
                });
        }
    }
}
