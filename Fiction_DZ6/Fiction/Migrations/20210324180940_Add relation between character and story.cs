using Microsoft.EntityFrameworkCore.Migrations;

namespace Fiction_DZ6.Migrations
{
    public partial class Addrelationbetweencharacterandstory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "StoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "StoryId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Story",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "LOTR" });

            migrationBuilder.UpdateData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3,
                column: "StoryId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_StoryId",
                table: "Characters",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Story_StoryId",
                table: "Characters",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Story_StoryId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_StoryId",
                table: "Characters");

            migrationBuilder.DeleteData(
                table: "Story",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Characters");
        }
    }
}
