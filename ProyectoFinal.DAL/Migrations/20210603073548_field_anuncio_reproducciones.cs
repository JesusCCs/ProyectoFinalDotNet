using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class field_anuncio_reproducciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("44ed72d9-91e0-4e58-bd49-564ac5117e2b"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("952c5113-a06c-43c5-ace6-eb22f1e5b5b0"));

            migrationBuilder.AlterColumn<int>(
                name: "reproduccionesLimite",
                table: "anuncios",
                type: "int(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reproducciones",
                table: "anuncios",
                type: "int(10)",
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bb982a3-125a-4cf2-99d9-9afe23955967", "AQAAAAEAACcQAAAAEC0mBUacSKuceLsKg4kUkEfzsonfn2oNxWtMPVffJOYmDkuRnojNNQGKd61yVVwQYA==", "43d40475-3d3b-4c70-aa32-a61cc8e4ef32" });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "2fb8cd29-c056-48cb-902f-7a99e93f3664");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("20889caa-227f-42a3-98ac-ab26f51fab17"), "1fb0b440-425a-4a90-b9f4-3aa7b058e83a", "Gimnasio", "GIMNASIO" },
                    { new Guid("99098188-e618-4c4c-a756-6f70ddeb2628"), "7a85a986-98aa-4bb2-909b-656e2e964bb7", "Usuario", "USUARIO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("20889caa-227f-42a3-98ac-ab26f51fab17"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("99098188-e618-4c4c-a756-6f70ddeb2628"));

            migrationBuilder.DropColumn(
                name: "reproducciones",
                table: "anuncios");

            migrationBuilder.AlterColumn<int>(
                name: "reproduccionesLimite",
                table: "anuncios",
                type: "int(2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(10)",
                oldNullable: true);

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
    }
}
