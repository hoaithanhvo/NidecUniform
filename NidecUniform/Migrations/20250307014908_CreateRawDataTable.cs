using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class CreateRawDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
        name: "RawData",
        columns: table => new
        {
            STTNo = table.Column<int>(nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"), // Identity column (auto-increment)
            MSNVID = table.Column<string>(nullable: true),
            FullName = table.Column<string>(nullable: true),
            Dept = table.Column<string>(nullable: true),
            Sex = table.Column<string>(nullable: true),
            UniformType = table.Column<string>(nullable: true),
            ReceivedDateTime = table.Column<DateTime>(nullable: false),
            NumberOfPaint = table.Column<int>(nullable: false),
            PaintType = table.Column<string>(nullable: true),
            NumberOfshirts = table.Column<int>(nullable: false),
            ShirtsType = table.Column<string>(nullable: true),
            NumberOfCones = table.Column<int>(nullable: false),
            ConesType = table.Column<string>(nullable: true),
            NumberOfShoes = table.Column<int>(nullable: false),
            ShoesType = table.Column<string>(nullable: true),
            SignReceived = table.Column<bool>(nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_RawData", x => x.STTNo); // Primary key on STTNo
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
       name: "RawData");
        }
    }
}
