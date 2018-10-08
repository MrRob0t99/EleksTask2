using Microsoft.EntityFrameworkCore.Migrations;

namespace EleksTask.Migrations
{
    public partial class AddedId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_AspNetUsers_ApplicationUserId",
                table: "BasketProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "ApolicationuserId",
                table: "BasketProducts");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "BasketProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BasketProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts",
                columns: new[] { "ProductId", "ApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_AspNetUsers_ApplicationUserId",
                table: "BasketProducts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_AspNetUsers_ApplicationUserId",
                table: "BasketProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasketProducts");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "BasketProducts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ApolicationuserId",
                table: "BasketProducts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts",
                columns: new[] { "ProductId", "ApolicationuserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_AspNetUsers_ApplicationUserId",
                table: "BasketProducts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
