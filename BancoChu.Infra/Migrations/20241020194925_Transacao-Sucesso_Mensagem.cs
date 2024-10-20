using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoChu.Infra.Migrations
{
    public partial class TransacaoSucesso_Mensagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mensagem",
                table: "Transacoes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sucesso",
                table: "Transacoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mensagem",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Sucesso",
                table: "Transacoes");
        }
    }
}
