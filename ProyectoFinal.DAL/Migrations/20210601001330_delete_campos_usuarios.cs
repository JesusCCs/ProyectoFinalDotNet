using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class delete_campos_usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("1896c21f-eebe-427d-a2ac-29416eb3432c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("b3707a12-3297-4b3b-845f-6758db4b4484"));

            migrationBuilder.DropColumn(
                name: "apellidos",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "direccion",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "usuarios");

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b87c109-0729-4e95-a00d-6b1d1f74305e", "AQAAAAEAACcQAAAAEPbGxjTPwH6ercXJzC0ktoqBymTCySJtpMpFtdzm/km+vTTH2lFEyNrRtQfkjeTd7w==", "1fa44407-33ba-45f4-8383-021821f541ea" });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "4735852d-57e3-4be5-b42b-133896976065");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("44ed72d9-91e0-4e58-bd49-564ac5117e2b"), "df23114a-8667-4783-9a03-df503f7afa36", "Gimnasio", "GIMNASIO" },
                    { new Guid("952c5113-a06c-43c5-ace6-eb22f1e5b5b0"), "0531e8d0-0e5b-4c7f-9c1a-478a42caa0fa", "Usuario", "USUARIO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("44ed72d9-91e0-4e58-bd49-564ac5117e2b"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("952c5113-a06c-43c5-ace6-eb22f1e5b5b0"));

            migrationBuilder.AddColumn<string>(
                name: "apellidos",
                table: "usuarios",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "direccion",
                table: "usuarios",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "usuarios",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d000d5d-0b96-4a1b-a214-9904d9d232d1", "AQAAAAEAACcQAAAAEBwW0m8jYuTXd7zv9VBFlURFYBue0zma2Qcu8OIZ2aH0+gpqki5TOQE5+BTS13+sdQ==", "22e83ec5-bfdc-4839-b1d8-f285eb546eef" });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "65792b89-679f-4057-ab25-4f251c9c8de7");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1896c21f-eebe-427d-a2ac-29416eb3432c"), "731d9180-5b54-4e11-af56-d0ef6a0fb227", "Gimnasio", "GIMNASIO" },
                    { new Guid("b3707a12-3297-4b3b-845f-6758db4b4484"), "cec9bab3-e9c4-41bc-a13c-6286b003716e", "Usuario", "USUARIO" }
                });
        }
    }
}
