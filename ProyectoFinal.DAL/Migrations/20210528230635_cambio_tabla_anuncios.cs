using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class cambio_tabla_anuncios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "fechaFin",
                table: "anuncios");

            migrationBuilder.DropColumn(
                name: "fechaInicio",
                table: "anuncios");

            migrationBuilder.AlterTable(
                name: "anuncios",
                oldComment: "La tabla que contiene los anuncios contratados por los gimnasios. Las especificaciones son las siguiente");

            migrationBuilder.AddColumn<DateTime>(
                name: "fin",
                table: "anuncios",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "inicio",
                table: "anuncios",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "auth",
                keyColumn: "Id",
                keyValue: new Guid("76148582-7877-4b91-be8e-22e34376045a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25b243e7-37b7-4272-a085-cb9e8bf8c182", "AQAAAAEAACcQAAAAEHWveiUlWe8aZasN74+KVvFEe+H9brzmhQ407E60Z3y06xfuskvqfGkRi1oIH8caQA==", "139396d8-02b6-4e86-92a5-66bad34c515b" });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("893feaeb-af9f-400c-8779-4c783f3986b4"),
                column: "ConcurrencyStamp",
                value: "1c1d764c-d9cf-4123-bd38-b80f29cd66d5");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("da57d690-85ea-47fe-8592-20ea711f7044"), "191ef885-0394-4cc4-b3e6-5ca3403733e4", "Gimnasio", "GIMNASIO" },
                    { new Guid("af7f36b5-cce8-4569-86f2-912672c9c6cb"), "f0a19413-cbbe-4518-a9c6-40ec47484013", "Usuario", "USUARIO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("af7f36b5-cce8-4569-86f2-912672c9c6cb"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("da57d690-85ea-47fe-8592-20ea711f7044"));

            migrationBuilder.DropColumn(
                name: "fin",
                table: "anuncios");

            migrationBuilder.DropColumn(
                name: "inicio",
                table: "anuncios");

            migrationBuilder.AlterTable(
                name: "anuncios",
                comment: "La tabla que contiene los anuncios contratados por los gimnasios. Las especificaciones son las siguiente");

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaFin",
                table: "anuncios",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaInicio",
                table: "anuncios",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
