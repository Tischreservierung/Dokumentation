using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tischreservierung.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfRestaurant",
                columns: table => new
                {
                    RestaurantType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfRestaurant", x => x.RestaurantType);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantTypeOfRestaurant",
                columns: table => new
                {
                    RestaurantTypesRestaurantType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTypeOfRestaurant", x => new { x.RestaurantTypesRestaurantType, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_RestaurantTypeOfRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantTypeOfRestaurant_TypeOfRestaurant_RestaurantTypesRestaurantType",
                        column: x => x.RestaurantTypesRestaurantType,
                        principalTable: "TypeOfRestaurant",
                        principalColumn: "RestaurantType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantTypeOfRestaurant_RestaurantsId",
                table: "RestaurantTypeOfRestaurant",
                column: "RestaurantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantTypeOfRestaurant");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "TypeOfRestaurant");
        }
    }
}
