using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Web_App.Migrations
{
    public partial class SeedNewsdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorName", "IsFake", "Text", "Title" },
                values: new object[,]
                {
                    { 1, "Jeremy Clarkson", true, "", "Humanity finally colonized the Mercury!!" },
                    { 2, "Svetlana Sokolova", true, "", "Increase your lifespan by 10 years, every morning you need..." },
                    { 3, "John Jones", false, "", "Scientists estimed the time of the vaccine invension: it is a summer of 2021" },
                    { 4, "Cerol Denvers", false, "", "Ukraine reduces the cost of its obligations!" },
                    { 5, "Jimmy Felon", true, "", "A species were discovered in Africa: it is blue legless cat" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
