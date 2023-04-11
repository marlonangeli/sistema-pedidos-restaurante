using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurante.Cheng.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GarcomId1",
                table: "Atendimentos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MesaId1",
                table: "Atendimentos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_GarcomId1",
                table: "Atendimentos",
                column: "GarcomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_MesaId1",
                table: "Atendimentos",
                column: "MesaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_Garcons_GarcomId1",
                table: "Atendimentos",
                column: "GarcomId1",
                principalTable: "Garcons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_Mesas_MesaId1",
                table: "Atendimentos",
                column: "MesaId1",
                principalTable: "Mesas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimentos_Garcons_GarcomId1",
                table: "Atendimentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Atendimentos_Mesas_MesaId1",
                table: "Atendimentos");

            migrationBuilder.DropIndex(
                name: "IX_Atendimentos_GarcomId1",
                table: "Atendimentos");

            migrationBuilder.DropIndex(
                name: "IX_Atendimentos_MesaId1",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "GarcomId1",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "MesaId1",
                table: "Atendimentos");
        }
    }
}
