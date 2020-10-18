using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AlteraçõesCarrinhoCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "CompraUsuario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ESTADO = table.Column<int>(nullable: false),
                    DATACOMPRA = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Compra_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraUsuario_CompraId",
                table: "CompraUsuario",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_UserId",
                table: "Compra",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraUsuario_Compra_CompraId",
                table: "CompraUsuario",
                column: "CompraId",
                principalTable: "Compra",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraUsuario_Compra_CompraId",
                table: "CompraUsuario");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_CompraUsuario_CompraId",
                table: "CompraUsuario");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "CompraUsuario");
        }
    }
}
