using Microsoft.EntityFrameworkCore.Migrations;

namespace Fiction_DZ6.Migrations
{
    public partial class Seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Age", "Name" },
                values: new object[] { 1, 14, "Finn Mertens" });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Age", "Name" },
                values: new object[] { 2, 25, "Philip Fry" });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Age", "Name" },
                values: new object[] { 3, 2700, "Arven Undomiel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
