using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorDrop.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<string>(maxLength: 2, nullable: false),
                    CountryNameEnglish = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<string>(maxLength: 2, nullable: false),
                    RegionNameEnglish = table.Column<string>(nullable: false),
                    CountryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 128, nullable: false),
                    CountryId = table.Column<string>(nullable: false),
                    RegionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryNameEnglish" },
                values: new object[] { "US", "United States of America" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryNameEnglish" },
                values: new object[] { "CA", "Canada" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionId", "CountryId", "RegionNameEnglish" },
                values: new object[,]
                {
                    { "CT", "US", "Connecticut" },
                    { "ME", "US", "Maine" },
                    { "MA", "US", "Massachusetts" },
                    { "NH", "US", "New Hampshire" },
                    { "RI", "US", "Rhode Island" },
                    { "VT", "US", "Vermont" },
                    { "NB", "CA", "New Brunswick" },
                    { "NF", "CA", "Newfoundland" },
                    { "NT", "CA", "Northwest Territories" },
                    { "NS", "CA", "Nova Scotia" },
                    { "NU", "CA", "Nunavut" },
                    { "ON", "CA", "Ontario" },
                    { "PE", "CA", "Prince Edward Island" },
                    { "QC", "CA", "Québec" },
                    { "SK", "CA", "Saskatchewan" },
                    { "YT", "CA", "Yukon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CountryId",
                table: "Customers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RegionId",
                table: "Customers",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
