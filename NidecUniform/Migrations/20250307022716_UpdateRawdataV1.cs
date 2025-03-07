using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRawdataV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "STTNo",
                table: "RawData",
                newName: "ID");
            migrationBuilder.RenameColumn(
             name: "MSNVID",
             table: "RawData",
             newName: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "STTNo",
                table: "RawData",
                newName: "STTNo");
            migrationBuilder.RenameColumn(
             name: "MSNVID",
             table: "RawData",
             newName: "MSNVID");
        }
    }
}
