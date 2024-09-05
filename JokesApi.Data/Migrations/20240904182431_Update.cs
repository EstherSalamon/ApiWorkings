using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JokeId",
                table: "FaveJokes");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "FaveJokes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "FaveJokes");

            migrationBuilder.AddColumn<int>(
                name: "JokeId",
                table: "FaveJokes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
