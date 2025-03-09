using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "DeliveryDetail",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetail_EmployeeID",
                table: "DeliveryDetail",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryDetail_M_Employee_EmployeeID",
                table: "DeliveryDetail",
                column: "EmployeeID",
                principalTable: "M_Employee",
                principalColumn: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryDetail_M_Employee_EmployeeID",
                table: "DeliveryDetail");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryDetail_EmployeeID",
                table: "DeliveryDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "DeliveryDetail");
        }
    }
}
