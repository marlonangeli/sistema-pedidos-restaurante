using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Cheng.Data.Migrations
{
    /// <inheritdoc />
    public partial class AtendimentoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtendimentoProduto_Atendimentos_AtendimentoId",
                table: "AtendimentoProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_AtendimentoProduto_Produtos_ProdutoId",
                table: "AtendimentoProduto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AtendimentoProduto",
                table: "AtendimentoProduto");

            migrationBuilder.RenameTable(
                name: "AtendimentoProduto",
                newName: "AtendimentoProdutos");

            migrationBuilder.RenameIndex(
                name: "IX_AtendimentoProduto_ProdutoId",
                table: "AtendimentoProdutos",
                newName: "IX_AtendimentoProdutos_ProdutoId");

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "AtendimentoProdutos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtendimentoProdutos",
                table: "AtendimentoProdutos",
                columns: new[] { "AtendimentoId", "ProdutoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AtendimentoProdutos_Atendimentos_AtendimentoId",
                table: "AtendimentoProdutos",
                column: "AtendimentoId",
                principalTable: "Atendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AtendimentoProdutos_Produtos_ProdutoId",
                table: "AtendimentoProdutos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtendimentoProdutos_Atendimentos_AtendimentoId",
                table: "AtendimentoProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_AtendimentoProdutos_Produtos_ProdutoId",
                table: "AtendimentoProdutos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AtendimentoProdutos",
                table: "AtendimentoProdutos");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "AtendimentoProdutos");

            migrationBuilder.RenameTable(
                name: "AtendimentoProdutos",
                newName: "AtendimentoProduto");

            migrationBuilder.RenameIndex(
                name: "IX_AtendimentoProdutos_ProdutoId",
                table: "AtendimentoProduto",
                newName: "IX_AtendimentoProduto_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtendimentoProduto",
                table: "AtendimentoProduto",
                columns: new[] { "AtendimentoId", "ProdutoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AtendimentoProduto_Atendimentos_AtendimentoId",
                table: "AtendimentoProduto",
                column: "AtendimentoId",
                principalTable: "Atendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AtendimentoProduto_Produtos_ProdutoId",
                table: "AtendimentoProduto",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
