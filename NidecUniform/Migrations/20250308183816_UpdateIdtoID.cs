using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdtoID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_Request_M_Employee_EmpolyeeId",
                table: "M_Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Product_ProductId",
                table: "RequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Request_RequestId",
                table: "RequestDetail");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "RequestDetail",
                newName: "RequestID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "RequestDetail",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "EmpolyeeId",
                table: "M_Request",
                newName: "EmpolyeeID");

            migrationBuilder.RenameIndex(
                name: "IX_MRequests_EmpolyeeId",
                table: "M_Request",
                newName: "IX_M_Request_EmpolyeeID");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "M_Delivery",
                newName: "RequestID");

            migrationBuilder.RenameColumn(
                name: "EmpolyeeId",
                table: "M_Delivery",
                newName: "EmpolyeeID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "DeliveryDetail",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "DeliveryId",
                table: "DeliveryDetail",
                newName: "DeliveryID");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "M_Request",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MRequests_EmpolyeeId",
                table: "M_Request",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_M_Request_M_Employee_EmpolyeeID",
                table: "M_Request",
                column: "EmpolyeeID",
                principalTable: "M_Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Product_ProductID",
                table: "RequestDetail",
                column: "ProductID",
                principalTable: "M_Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Request_RequestID",
                table: "RequestDetail",
                column: "RequestID",
                principalTable: "M_Request",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_Request_M_Employee_EmpolyeeID",
                table: "M_Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Product_ProductID",
                table: "RequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Request_RequestID",
                table: "RequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_MRequests_EmpolyeeId",
                table: "M_Request");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "M_Request");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "RequestDetail",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "RequestDetail",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "EmpolyeeID",
                table: "M_Request",
                newName: "EmpolyeeId");

            migrationBuilder.RenameIndex(
                name: "IX_M_Request_EmpolyeeID",
                table: "M_Request",
                newName: "IX_MRequests_EmpolyeeId");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "M_Delivery",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "EmpolyeeID",
                table: "M_Delivery",
                newName: "EmpolyeeId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "DeliveryDetail",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "DeliveryID",
                table: "DeliveryDetail",
                newName: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_M_Request_M_Employee_EmpolyeeId",
                table: "M_Request",
                column: "EmpolyeeId",
                principalTable: "M_Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Product_ProductId",
                table: "RequestDetail",
                column: "ProductId",
                principalTable: "M_Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Request_RequestId",
                table: "RequestDetail",
                column: "RequestId",
                principalTable: "M_Request",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
