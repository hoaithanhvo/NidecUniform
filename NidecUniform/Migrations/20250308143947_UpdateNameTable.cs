using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetails_MProducts_ProductId",
                table: "RequestDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetails_MRequests_RequestId",
                table: "RequestDetails");

            migrationBuilder.DropTable(
                name: "MDeliveries");

            migrationBuilder.DropTable(
                name: "MProducts");

            migrationBuilder.DropTable(
                name: "MRequests");

            migrationBuilder.DropTable(
                name: "MEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestDetails",
                table: "RequestDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryDetails",
                table: "DeliveryDetails");

            migrationBuilder.RenameTable(
                name: "RequestDetails",
                newName: "RequestDetail");

            migrationBuilder.RenameTable(
                name: "DeliveryDetails",
                newName: "DeliveryDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestDetail",
                table: "RequestDetail",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryDetail",
                table: "DeliveryDetail",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "M_Delivery",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Delivery", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M_Employee",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Employee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M_Product",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Product", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RawData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dept = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniformType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NumberOfPaint = table.Column<int>(type: "int", nullable: false),
                    PaintType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfshirts = table.Column<int>(type: "int", nullable: false),
                    ShirtsType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfCones = table.Column<int>(type: "int", nullable: false),
                    ConesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfShoes = table.Column<int>(type: "int", nullable: false),
                    ShoesType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignReceived = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M_Request",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Request", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_Request_M_Employee_EmpolyeeId",
                        column: x => x.EmpolyeeId,
                        principalTable: "M_Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MRequests_EmpolyeeId",
                table: "M_Request",
                column: "EmpolyeeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Product_ProductId",
                table: "RequestDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDetail_M_Request_RequestId",
                table: "RequestDetail");

            migrationBuilder.DropTable(
                name: "M_Delivery");

            migrationBuilder.DropTable(
                name: "M_Product");

            migrationBuilder.DropTable(
                name: "M_Request");

            migrationBuilder.DropTable(
                name: "RawData");

            migrationBuilder.DropTable(
                name: "M_Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestDetail",
                table: "RequestDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryDetail",
                table: "DeliveryDetail");

            migrationBuilder.RenameTable(
                name: "RequestDetail",
                newName: "RequestDetails");

            migrationBuilder.RenameTable(
                name: "DeliveryDetail",
                newName: "DeliveryDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestDetails",
                table: "RequestDetails",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryDetails",
                table: "DeliveryDetails",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "MDeliveries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDeliveries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MEmployees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmpolyeeId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEmployees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ProductID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MProducts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MRequests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MRequests_MEmployees_EmpolyeeId",
                        column: x => x.EmpolyeeId,
                        principalTable: "MEmployees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MRequests_EmpolyeeId",
                table: "MRequests",
                column: "EmpolyeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetails_MProducts_ProductId",
                table: "RequestDetails",
                column: "ProductId",
                principalTable: "MProducts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDetails_MRequests_RequestId",
                table: "RequestDetails",
                column: "RequestId",
                principalTable: "MRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
