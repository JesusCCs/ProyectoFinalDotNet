using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoFinal.DAL.Migrations
{
    public partial class add_logo_field_to_gimnasios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "logo",
                table: "gimnasios",
                type: "varchar(255)",
                nullable: true,
                comment: "Campo útil para tener una referencia al nombre con el que se guardó el fichero subido por el usuario en wwwroot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logo",
                table: "gimnasios");
        }
    }
}
