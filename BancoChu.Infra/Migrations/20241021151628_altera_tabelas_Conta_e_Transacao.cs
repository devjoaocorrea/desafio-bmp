using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoChu.Infra.Migrations
{
    public partial class altera_tabelas_Conta_e_Transacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mensagem",
                table: "Transacoes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titular",
                table: "Contas",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ChavePix",
                table: "Contas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Contas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChavePix",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Contas");

            migrationBuilder.AlterColumn<string>(
                name: "Mensagem",
                table: "Transacoes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titular",
                table: "Contas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);
        }
    }
}
