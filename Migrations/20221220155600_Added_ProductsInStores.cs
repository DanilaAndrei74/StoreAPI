using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductsInStores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInStores_Products_ProductId",
                table: "ProductInStores");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInStores_Stores_StoreId",
                table: "ProductInStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInStores",
                table: "ProductInStores");

            migrationBuilder.RenameTable(
                name: "ProductInStores",
                newName: "ProductsInStores");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInStores_StoreId",
                table: "ProductsInStores",
                newName: "IX_ProductsInStores_StoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsInStores",
                table: "ProductsInStores",
                columns: new[] { "ProductId", "StoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInStores_Products_ProductId",
                table: "ProductsInStores",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInStores_Stores_StoreId",
                table: "ProductsInStores",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInStores_Products_ProductId",
                table: "ProductsInStores");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInStores_Stores_StoreId",
                table: "ProductsInStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsInStores",
                table: "ProductsInStores");

            migrationBuilder.RenameTable(
                name: "ProductsInStores",
                newName: "ProductInStores");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsInStores_StoreId",
                table: "ProductInStores",
                newName: "IX_ProductInStores_StoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInStores",
                table: "ProductInStores",
                columns: new[] { "ProductId", "StoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInStores_Products_ProductId",
                table: "ProductInStores",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInStores_Stores_StoreId",
                table: "ProductInStores",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
