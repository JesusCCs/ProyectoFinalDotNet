using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class add_recurso_field_to_anuncios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "recurso",
                table: "anuncios",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                comment: "Se guarda la referencia al archivo en la carpeta anuncios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recurso",
                table: "anuncios");
        }
    }
}
