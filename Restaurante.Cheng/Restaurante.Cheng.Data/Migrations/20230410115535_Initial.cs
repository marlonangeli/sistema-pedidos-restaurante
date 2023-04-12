using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Cheng.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Nome = table.Column<string>(type: "TEXT", nullable: false),
                        Descricao = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Garcons",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Nome = table.Column<string>(type: "TEXT", nullable: false),
                        Sobrenome = table.Column<string>(type: "TEXT", nullable: false),
                        NumeroTelefone = table.Column<string>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garcons", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Numero = table.Column<int>(type: "INTEGER", nullable: false),
                        Status = table.Column<int>(type: "INTEGER", nullable: false),
                        HoraAbertura = table.Column<DateTime>(type: "TEXT", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Nome = table.Column<string>(type: "TEXT", nullable: false),
                        Descricao = table.Column<string>(type: "TEXT", nullable: false),
                        Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                        CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        MesaId = table.Column<int>(type: "INTEGER", nullable: false),
                        GarcomId = table.Column<int>(type: "INTEGER", nullable: false),
                        HorarioPedido = table.Column<DateTime>(type: "TEXT", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Garcons_GarcomId",
                        column: x => x.GarcomId,
                        principalTable: "Garcons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Atendimentos_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AtendimentoProduto",
                columns: table =>
                    new
                    {
                        AtendimentoId = table.Column<int>(type: "INTEGER", nullable: false),
                        ProdutoId = table.Column<int>(type: "INTEGER", nullable: false),
                        Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_AtendimentoProduto",
                        x => new { x.AtendimentoId, x.ProdutoId }
                    );
                    table.ForeignKey(
                        name: "FK_AtendimentoProduto_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_AtendimentoProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_AtendimentoProduto_ProdutoId",
                table: "AtendimentoProduto",
                column: "ProdutoId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_GarcomId",
                table: "Atendimentos",
                column: "GarcomId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_MesaId",
                table: "Atendimentos",
                column: "MesaId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AtendimentoProduto");

            migrationBuilder.DropTable(name: "Atendimentos");

            migrationBuilder.DropTable(name: "Produtos");

            migrationBuilder.DropTable(name: "Garcons");

            migrationBuilder.DropTable(name: "Mesas");

            migrationBuilder.DropTable(name: "Categorias");
        }
    }
}
