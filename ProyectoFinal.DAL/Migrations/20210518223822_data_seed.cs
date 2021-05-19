using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class data_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "auth",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("76148582-7877-4b91-be8e-22e34376045a"), 0, "e6d121f7-e67d-4e1c-bbfc-c8194e3e581c", "admin@email.es", true, false, null, "admin@email.es", "admin", "AQAAAAEAACcQAAAAEEQqDxaZ/rV+g/vMlSK7zAfor84LsTsRmL0TDOBZn41iakEfJlMLKvRLwk8sMP1LRw==", null, false, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"), "01cd6466-5318-4e3a-9270-eddc5370154f", "Admin", "ADMIN" },
                    { new Guid("6192df98-7aad-41db-8501-056cde66ea9b"), "4b0005bb-c6e0-4e95-8595-0d4abfbbc9b6", "Gimnasio", "GIMNASIO" },
                    { new Guid("2a01e75a-8b8c-4f8b-b221-ae90074a776f"), "4a644594-8967-4c21-8c63-763cb6943074", "Usuario", "USUARIO" }
                });

            migrationBuilder.InsertData(
                table: "auth_roles",
                columns: new[] { "RoleId", "AuthId" },
                values: new object[] { new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"), new Guid("76148582-7877-4b91-be8e-22e34376045a") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "auth_roles",
                keyColumns: new[] { "RoleId", "AuthId" },
                keyValues: new object[] { new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"), new Guid("76148582-7877-4b91-be8e-22e34376045a") });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2a01e75a-8b8c-4f8b-b221-ae90074a776f"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("6192df98-7aad-41db-8501-056cde66ea9b"));

            migrationBuilder.DeleteData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"));
        }
    }
}
