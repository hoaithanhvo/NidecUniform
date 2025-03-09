using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class updateAllDesignDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_M_Product",
                table: "M_Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_M_Employee",
                table: "M_Employee");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "M_Product");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "M_Employee");

            migrationBuilder.DropColumn(
                name: "EmpolyeeID",
                table: "M_Delivery");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "RequestDetail",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "RequestDetail",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "M_DeliveryID",
                table: "RequestDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                table: "M_Request",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "M_Product",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "M_Delivery",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "DeliveryDetail",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "M_RequestID",
                table: "DeliveryDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestDetailID",
                table: "DeliveryDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_Product",
                table: "M_Product",
                column: "ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_Employee",
                table: "M_Employee",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetail_EmployeeID",
                table: "RequestDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetail_M_DeliveryID",
                table: "RequestDetail",
                column: "M_DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetail_ProductID",
                table: "RequestDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_M_Delivery_EmployeeID",
                table: "M_Delivery",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_M_Delivery_RequestID",
                table: "M_Delivery",
                column: "RequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetail_DeliveryID",
                table: "DeliveryDetail",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetail_M_RequestID",
                table: "DeliveryDetail",
                column: "M_RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetail_ProductID",
                table: "DeliveryDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetail_RequestDetailID",
                table: "DeliveryDetail",
                column: "RequestDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetail_M_Delivery_DeliveryID",
                table: "DeliveryDetail",
                column: "DeliveryID",
                principalTable: "M_Delivery",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetail_M_Product_ProductID",
                table: "DeliveryDetail",
                column: "ProductID",
                principalTable: "M_Product",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetail_M_Request_M_RequestID",
                table: "DeliveryDetail",
                column: "M_RequestID",
                principalTable: "M_Request",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetail_RequestDetail_RequestDetailID",
                table: "DeliveryDetail",
                column: "RequestDetailID",
                principalTable: "RequestDetail",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_M_Delivery_M_Employee_EmployeeID",
                table: "M_Delivery",
                column: "EmployeeID",
                principalTable: "M_Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_M_Delivery_M_Request_RequestID",
                table: "M_Delivery",
                column: "RequestID",
                principalTable: "M_Request",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_M_Request_M_Employee_EmployeeID",
                table: "M_Request",
                column: "EmployeeID",
                principalTable: "M_Employee",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Delivery_M_DeliveryID",
                table: "RequestDetail",
                column: "M_DeliveryID",
                principalTable: "M_Delivery",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Employee_EmployeeID",
                table: "RequestDetail",
                column: "EmployeeID",
                principalTable: "M_Employee",
                principalColumn: "EmployeeID");

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
                name: "FK_DeliveryDetail_M_Delivery_DeliveryID",
                table: "DeliveryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetail_M_Product_ProductID",
                table: "DeliveryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetail_M_Request_M_RequestID",
                table: "DeliveryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetail_RequestDetail_RequestDetailID",
                table: "DeliveryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_M_Delivery_M_Employee_EmployeeID",
                table: "M_Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_M_Delivery_M_Request_RequestID",
                table: "M_Delivery");

            migrationBuilder.DropForeignKey(
                name: "FK_M_Request_M_Employee_EmployeeID",
                table: "M_Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Delivery_M_DeliveryID",
                table: "RequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Employee_EmployeeID",
                table: "RequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Product_ProductID",
                table: "RequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_RequestDetail_EmployeeID",
                table: "RequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_RequestDetail_M_DeliveryID",
                table: "RequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_RequestDetail_ProductID",
                table: "RequestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_M_Product",
                table: "M_Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_M_Employee",
                table: "M_Employee");

            migrationBuilder.DropIndex(
                name: "IX_M_Delivery_EmployeeID",
                table: "M_Delivery");

            migrationBuilder.DropIndex(
                name: "IX_M_Delivery_RequestID",
                table: "M_Delivery");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryDetail_DeliveryID",
                table: "DeliveryDetail");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryDetail_M_RequestID",
                table: "DeliveryDetail");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryDetail_ProductID",
                table: "DeliveryDetail");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryDetail_RequestDetailID",
                table: "DeliveryDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "RequestDetail");

            migrationBuilder.DropColumn(
                name: "M_DeliveryID",
                table: "RequestDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "M_Delivery");

            migrationBuilder.DropColumn(
                name: "M_RequestID",
                table: "DeliveryDetail");

            migrationBuilder.DropColumn(
                name: "RequestDetailID",
                table: "DeliveryDetail");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "RequestDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                table: "M_Request",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "M_Product",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "M_Product",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "M_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EmpolyeeID",
                table: "M_Delivery",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "DeliveryDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_Product",
                table: "M_Product",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_Employee",
                table: "M_Employee",
                column: "ID");
        }
    }
}
