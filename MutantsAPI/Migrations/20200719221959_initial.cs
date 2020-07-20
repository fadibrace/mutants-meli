using Microsoft.EntityFrameworkCore.Migrations;

namespace MutantsAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genomes",
                columns: table => new
                {
                    DnaHash = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    DnaSequence = table.Column<string>(type: "varchar(max)", nullable: false),
                    IsMutant = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genomes", x => x.DnaHash);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genomes");
        }
    }
}
