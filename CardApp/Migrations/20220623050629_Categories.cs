using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardApp.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Category_CategoryId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_OwnerId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CategoryId",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Category_OwnerId",
                table: "Categories",
                newName: "IX_Categories_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "17a896d7-8b9a-40dc-a31e-352791eb7867", "AQAAAAEAACcQAAAAEImrZOaSizAJxU+gCKf/N/HT4gmHdzsTNr3m4JfSLiQNRLgVW7ExFv4HUpQbNxduWw==" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[] { 1L, "Food", 1L });

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Categories_OwnerId",
                table: "Cards",
                column: "OwnerId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_OwnerId",
                table: "Categories",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Categories_OwnerId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_OwnerId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_OwnerId",
                table: "Category",
                newName: "IX_Category_OwnerId");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Cards",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "06798ea7-73fd-4794-b240-64036be003f7", "AQAAAAEAACcQAAAAELssO+NNZNsqlHhkBCAzBq35tRfPT+cjB5KWmVS2Q1dHJBvzrMADvQInhWfJ4va5Vw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CategoryId",
                table: "Cards",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Category_CategoryId",
                table: "Cards",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_OwnerId",
                table: "Category",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
