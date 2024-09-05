using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JokeId",
                table: "FaveJokes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JokeId",
                table: "FaveJokes");
        }
    }
}
