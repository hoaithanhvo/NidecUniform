using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_RequestDetails_ProductId",
                table: "RequestDetail",
                newName: "IX_RequestDetail_ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_RequestDetail_ProductID",
                table: "RequestDetail",
                newName: "IX_RequestDetails_ProductId");
        }
    }
}
