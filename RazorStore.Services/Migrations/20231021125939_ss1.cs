using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorStore.Services.Migrations
{
    /// <inheritdoc />
    public partial class ss1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PathItem_Goods_GoodsId",
                table: "PathItem");

            migrationBuilder.RenameColumn(
                name: "GoodsId",
                table: "PathItem",
                newName: "GoodId");

            migrationBuilder.RenameIndex(
                name: "IX_PathItem_GoodsId",
                table: "PathItem",
                newName: "IX_PathItem_GoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_PathItem_Goods_GoodId",
                table: "PathItem",
                column: "GoodId",
                principalTable: "Goods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PathItem_Goods_GoodId",
                table: "PathItem");

            migrationBuilder.RenameColumn(
                name: "GoodId",
                table: "PathItem",
                newName: "GoodsId");

            migrationBuilder.RenameIndex(
                name: "IX_PathItem_GoodId",
                table: "PathItem",
                newName: "IX_PathItem_GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PathItem_Goods_GoodsId",
                table: "PathItem",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id");
        }
    }
}
