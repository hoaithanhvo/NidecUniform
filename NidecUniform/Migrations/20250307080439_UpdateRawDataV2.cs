using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRawDataV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "RawData",
                newName: "Gender");
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "RawData");
            migrationBuilder.RenameColumn(
                name: "ReceivedDateTime",
                table: "RawData",
                newName: "StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "Gender",
               table: "RawData",
               newName: "Sex");

            // Xóa cột EndDate (chỉ rollback nếu cần)
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RawData");

            // Đổi lại tên cột từ StartDate -> ReceivedDateTime
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "RawData",
                newName: "ReceivedDateTime");
        }
    }
}
