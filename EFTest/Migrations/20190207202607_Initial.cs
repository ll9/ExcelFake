using Microsoft.EntityFrameworkCore.Migrations;

namespace EFTest.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SDDataTables",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValueSql: "HEX(RANDOMBLOB(16))"),
                    Name = table.Column<string>(nullable: true),
                    Synchronize = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDDataTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SDProjects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValueSql: "HEX(RANDOMBLOB(16))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SDColumns",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValueSql: "HEX(RANDOMBLOB(16))"),
                    Name = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    Synchronize = table.Column<bool>(nullable: false),
                    SDDataTableId = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ComboboxValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SDColumns_SDDataTables_SDDataTableId",
                        column: x => x.SDDataTableId,
                        principalTable: "SDDataTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SDColumns_SDDataTableId",
                table: "SDColumns",
                column: "SDDataTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SDColumns");

            migrationBuilder.DropTable(
                name: "SDProjects");

            migrationBuilder.DropTable(
                name: "SDDataTables");
        }
    }
}
