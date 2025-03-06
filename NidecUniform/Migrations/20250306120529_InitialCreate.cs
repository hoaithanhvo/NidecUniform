using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NidecUniform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "M_Employees",
                columns: table => new
                {
                    Empolyee_ID = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Empolyee", x => x.Empolyee_ID);
                });

            migrationBuilder.CreateTable(
                name: "M_Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M_Requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empolyee_ID = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__M_Reques__33A8519A60486747", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_Requests_Employee",
                        column: x => x.Empolyee_ID,
                        principalTable: "M_Employees",
                        principalColumn: "Empolyee_ID");
                });

            migrationBuilder.CreateTable(
                name: "M_Deliveries",
                columns: table => new
                {
                    DeliveryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    Empolyee_ID = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DeliveredBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Deliveri__626D8FEED074CDB0", x => x.DeliveryID);
                    table.ForeignKey(
                        name: "FK_Deliveries_Employee",
                        column: x => x.Empolyee_ID,
                        principalTable: "M_Employees",
                        principalColumn: "Empolyee_ID");
                    table.ForeignKey(
                        name: "FK_Deliveries_Request",
                        column: x => x.RequestID,
                        principalTable: "M_Requests",
                        principalColumn: "RequestID");
                });

            migrationBuilder.CreateTable(
                name: "RequestDetails",
                columns: table => new
                {
                    DetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    QuantityRequested = table.Column<int>(type: "int", nullable: false),
                    QuantityApproved = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    QuantityDelivered = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RequestD__135C314DC9F0A3FF", x => x.DetailID);
                    table.ForeignKey(
                        name: "FK_RequestDetails_Product",
                        column: x => x.ProductID,
                        principalTable: "M_Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RequestDetails_Request",
                        column: x => x.RequestID,
                        principalTable: "M_Requests",
                        principalColumn: "RequestID");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDetails",
                columns: table => new
                {
                    DeliveryDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    QuantityDelivered = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__EFD2C287B58D7321", x => x.DeliveryDetailID);
                    table.ForeignKey(
                        name: "FK_DeliveryDetails_Delivery",
                        column: x => x.DeliveryID,
                        principalTable: "M_Deliveries",
                        principalColumn: "DeliveryID");
                    table.ForeignKey(
                        name: "FK_DeliveryDetails_Product",
                        column: x => x.ProductID,
                        principalTable: "M_Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_DeliveryID",
                table: "DeliveryDetails",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDetails_ProductID",
                table: "DeliveryDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_M_Deliveries_Empolyee_ID",
                table: "M_Deliveries",
                column: "Empolyee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_M_Deliveries_RequestID",
                table: "M_Deliveries",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_M_Requests_Empolyee_ID",
                table: "M_Requests",
                column: "Empolyee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_ProductID",
                table: "RequestDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_RequestID",
                table: "RequestDetails",
                column: "RequestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryDetails");

            migrationBuilder.DropTable(
                name: "RequestDetails");

            migrationBuilder.DropTable(
                name: "M_Deliveries");

            migrationBuilder.DropTable(
                name: "M_Products");

            migrationBuilder.DropTable(
                name: "M_Requests");

            migrationBuilder.DropTable(
                name: "M_Employees");
        }
    }
}
