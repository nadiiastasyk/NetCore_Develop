using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailBar.Migrations
{
    public partial class UpdatedOrderstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cocktail",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PayByCard",
                table: "Orders",
                newName: "IsActive");

            migrationBuilder.CreateTable(
                name: "Cocktail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cocktail_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cocktail",
                columns: new[] { "Id", "Image", "Ingredients", "Name", "OrderId" },
                values: new object[] { 1, null, null, "Smashed Watermelon Margarita", 1 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_Cocktail_OrderId",
                table: "Cocktail",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cocktail");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Orders",
                newName: "PayByCard");

            migrationBuilder.AddColumn<string>(
                name: "Cocktail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cocktail", "PayByCard" },
                values: new object[] { "Negroni", true });
        }
    }
}
