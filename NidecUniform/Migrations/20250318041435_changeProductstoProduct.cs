using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class changeProductstoProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetail_M_Products_ProductID",
                table: "DeliveryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Products_ProductID",
                table: "RequestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_M_Products",
                table: "M_Products");

            migrationBuilder.RenameTable(
                name: "M_Products",
                newName: "M_Product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_Product",
                table: "M_Product",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetail_M_Product_ProductID",
                table: "DeliveryDetail",
                column: "ProductID",
                principalTable: "M_Product",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Product_ProductID",
                table: "RequestDetail",
                column: "ProductID",
                principalTable: "M_Product",
                principalColumn: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetail_M_Product_ProductID",
                table: "DeliveryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Product_ProductID",
                table: "RequestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_M_Product",
                table: "M_Product");

            migrationBuilder.RenameTable(
                name: "M_Product",
                newName: "M_Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_Products",
                table: "M_Products",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetail_M_Products_ProductID",
                table: "DeliveryDetail",
                column: "ProductID",
                principalTable: "M_Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Products_ProductID",
                table: "RequestDetail",
                column: "ProductID",
                principalTable: "M_Products",
                principalColumn: "ProductID");
        }
    }
}
