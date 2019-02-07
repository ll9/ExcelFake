using Microsoft.EntityFrameworkCore.Migrations;

namespace EFTest.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false, defaultValueSql: "HEX(RANDOMBLOB(16))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

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
                name: "Columns",
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
                    table.PrimaryKey("PK_Columns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Columns_SDDataTables_SDDataTableId",
                        column: x => x.SDDataTableId,
                        principalTable: "SDDataTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columns_SDDataTableId",
                table: "Columns",
                column: "SDDataTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "SDDataTables");
        }
    }
}
