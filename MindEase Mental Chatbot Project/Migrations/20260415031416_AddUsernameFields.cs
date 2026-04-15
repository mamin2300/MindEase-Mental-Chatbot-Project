using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindEase_Mental_Chatbot_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "MoodEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "MoodEntries");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ChatMessages");
        }
    }
}
