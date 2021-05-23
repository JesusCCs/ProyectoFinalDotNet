using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class add_identifcador_tour_to_gimnasio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("2141cdf3-b3f0-40e7-9c9d-80d82fdc4cc1"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("628dd1f1-bdfb-4a74-bcde-0a3031e74407"));

            migrationBuilder.AddColumn<string>(
                name: "identificador",
                table: "gimnasios",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "recibido_tour",
                table: "gimnasios",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "'0'");

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e5eb8ed-b985-4aae-9287-b175faf02ed8", "AQAAAAEAACcQAAAAEBdBdJWR8gdjR9TTbotDUREQRnn/NSG84o5o6YEI/MqGWhHvDq1Ph3lTNPg08kEJ9g==", "bda9ae08-d855-4f33-8f30-72c54906bd0d" });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "e74ded5f-0055-40a5-acbb-e62d1060fdb9");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("bebb099f-44ef-4f41-8558-5b12f413ee1a"), "199aaba8-e5c1-42b4-9842-ec7da79bc4be", "Gimnasio", "GIMNASIO" },
                    { new Guid("e72c7d9b-4e58-4b81-9341-0ce6f46243e9"), "9436af0a-7f82-4d81-9b81-90b55e075516", "Usuario", "USUARIO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("bebb099f-44ef-4f41-8558-5b12f413ee1a"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("e72c7d9b-4e58-4b81-9341-0ce6f46243e9"));

            migrationBuilder.DropColumn(
                name: "identificador",
                table: "gimnasios");

            migrationBuilder.DropColumn(
                name: "recibido_tour",
                table: "gimnasios");

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

            migrationBuilder.CreateIndex(
                name: "gimnasios_cif_uindex",
                table: "gimnasios",
                column: "cif",
                unique: true);
        }
    }
}
