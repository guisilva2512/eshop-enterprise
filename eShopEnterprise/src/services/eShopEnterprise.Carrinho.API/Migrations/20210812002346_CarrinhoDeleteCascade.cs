using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopEnterprise.Carrinho.API.Migrations
{
    public partial class CarrinhoDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItem_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItem");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItem_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItem",
                column: "CarrinhoId",
                principalTable: "CarrinhoCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItem_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItem");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItem_CarrinhoCliente_CarrinhoId",
                table: "CarrinhoItem",
                column: "CarrinhoId",
                principalTable: "CarrinhoCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
