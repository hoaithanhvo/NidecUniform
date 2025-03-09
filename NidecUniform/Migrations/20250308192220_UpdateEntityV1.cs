using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_Request_M_Employee_EmpolyeeID",
                table: "M_Request");

            migrationBuilder.DropIndex(
                name: "IX_M_Request_EmpolyeeID",
                table: "M_Request");

            migrationBuilder.DropColumn(
                name: "EmpolyeeID",
                table: "M_Request");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpolyeeID",
                table: "M_Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_M_Request_EmpolyeeID",
                table: "M_Request",
                column: "EmpolyeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_M_Request_M_Employee_EmpolyeeID",
                table: "M_Request",
                column: "EmpolyeeID",
                principalTable: "M_Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
