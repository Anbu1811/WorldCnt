using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldCountry.API.Migrations
{
    /// <inheritdoc />
    public partial class states : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<double>(type: "float", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllStates_AllCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "AllCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllStates_CountryId",
                table: "AllStates",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllStates");
        }
    }
}
