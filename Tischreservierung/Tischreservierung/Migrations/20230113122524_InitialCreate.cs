using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tischreservierung.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantCategory",
                columns: table => new
                {
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantCategory", x => x.Category);
                });

            migrationBuilder.CreateTable(
                name: "Zipcodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipCodeNr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zipcodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCodeId = table.Column<int>(type: "int", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Zipcodes_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "Zipcodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RestaurantOpeningTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    OpeningTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantOpeningTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantOpeningTimes_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantRestaurantCategory",
                columns: table => new
                {
                    RestaurantTypesCategory = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantRestaurantCategory", x => new { x.RestaurantTypesCategory, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_RestaurantRestaurantCategory_RestaurantCategory_RestaurantTypesCategory",
                        column: x => x.RestaurantTypesCategory,
                        principalTable: "RestaurantCategory",
                        principalColumn: "Category",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantRestaurantCategory_Restaurants_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatPlaces = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantTables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantOpeningTimes_RestaurantId",
                table: "RestaurantOpeningTimes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantRestaurantCategory_RestaurantsId",
                table: "RestaurantRestaurantCategory",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_ZipCodeId",
                table: "Restaurants",
                column: "ZipCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantTables_RestaurantId",
                table: "RestaurantTables",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantOpeningTimes");

            migrationBuilder.DropTable(
                name: "RestaurantRestaurantCategory");

            migrationBuilder.DropTable(
                name: "RestaurantTables");

            migrationBuilder.DropTable(
                name: "RestaurantCategory");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Zipcodes");
        }
    }
}
