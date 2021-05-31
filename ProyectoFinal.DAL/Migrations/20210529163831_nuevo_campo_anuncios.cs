using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class nuevo_campo_anuncios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("af7f36b5-cce8-4569-86f2-912672c9c6cb"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("da57d690-85ea-47fe-8592-20ea711f7044"));

            migrationBuilder.AddColumn<bool>(
                name: "finalizado",
                table: "anuncios",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "'0'",
                comment: "Nos indica si este registro es de un anuncio cuya creación ha sido finalizada o no");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("9edc4db8-690f-48d9-989c-8f20aac3ad4d"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("dcc87d86-1910-4027-b97c-d1adf53375f6"));

            migrationBuilder.DropColumn(
                name: "finalizado",
                table: "anuncios");

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
    }
}
