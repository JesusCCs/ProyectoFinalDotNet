using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class cambios_permisos_null_anuncios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("9edc4db8-690f-48d9-989c-8f20aac3ad4d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("dcc87d86-1910-4027-b97c-d1adf53375f6"));

            migrationBuilder.AlterColumn<int>(
                name: "reproduccionesLimite",
                table: "anuncios",
                type: "int(2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int(2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "inicio",
                table: "anuncios",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fin",
                table: "anuncios",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("1896c21f-eebe-427d-a2ac-29416eb3432c"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("b3707a12-3297-4b3b-845f-6758db4b4484"));

            migrationBuilder.AlterColumn<int>(
                name: "reproduccionesLimite",
                table: "anuncios",
                type: "int(2)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "inicio",
                table: "anuncios",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fin",
                table: "anuncios",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c46c143f-5940-461f-8625-59c487cb624a", "AQAAAAEAACcQAAAAECpVcmdkzkSppM/kQch9QSJa9iFa2FDbfPySwrTVTXY7EoZnoUEWQeg8c1nUfGu+kg==", "af7437eb-658c-48e3-af81-aa27d4fb06d0" });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "11c09e61-f0c1-41b6-86eb-6dfc86518dd1");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9edc4db8-690f-48d9-989c-8f20aac3ad4d"), "07bf6e9b-e853-4766-8cdb-3d42bfa40b88", "Gimnasio", "GIMNASIO" },
                    { new Guid("dcc87d86-1910-4027-b97c-d1adf53375f6"), "fdfad6e1-9c46-43be-a72c-827e434ea634", "Usuario", "USUARIO" }
                });
        }
    }
}
