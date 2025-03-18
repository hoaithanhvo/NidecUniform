using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class dropMProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "M_Products",
                newName: "ProductVietnameseName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "RequestDetail",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "DeliveryDetail",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "M_Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "M_Products",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductEnglishName",
                table: "M_Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "M_Products");

            migrationBuilder.DropColumn(
                name: "ProductEnglishName",
                table: "M_Products");

            migrationBuilder.RenameTable(
                name: "M_Products",
                newName: "M_Product");

            migrationBuilder.RenameColumn(
                name: "ProductVietnameseName",
                table: "M_Product",
                newName: "ProductName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "RequestDetail",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "DeliveryDetail",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "M_Product",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
    }
}
