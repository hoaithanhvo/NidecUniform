using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Product_ProductID",
                table: "RequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_RequestDetail_ProductID",
                table: "RequestDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RequestDetail_ProductID",
                table: "RequestDetail",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetail_M_Product_ProductID",
                table: "RequestDetail",
                column: "ProductID",
                principalTable: "M_Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
