using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorStore.Services.Migrations
{
    /// <inheritdoc />
    public partial class ssssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Goods",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_UserId",
                table: "Goods",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_AspNetUsers_UserId",
                table: "Goods",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_AspNetUsers_UserId",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_UserId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Goods");
        }
    }
}
