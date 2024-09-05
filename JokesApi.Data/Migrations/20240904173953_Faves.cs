using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Faves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaveJokes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetUp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PunchLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JokeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaveJokes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaveJokes");
        }
    }
}
