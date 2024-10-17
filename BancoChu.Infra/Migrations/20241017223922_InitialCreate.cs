using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoChu.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NumeroConta = table.Column<string>(type: "TEXT", nullable: false),
                    Agencia = table.Column<string>(type: "TEXT", nullable: false),
                    Titular = table.Column<string>(type: "TEXT", nullable: false),
                    Saldo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaOrigemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaDestinoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Contas_ContaDestinoId",
                        column: x => x.ContaDestinoId,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacoes_Contas_ContaOrigemId",
                        column: x => x.ContaOrigemId,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_ContaDestinoId",
                table: "Transacoes",
                column: "ContaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_ContaOrigemId",
                table: "Transacoes",
                column: "ContaOrigemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Contas");
        }
    }
}
