using Microsoft.EntityFrameworkCore.Migrations;

namespace EFTest.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SDStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SDDataTables",
                columns: new[] { "Id", "Name", "Synchronize" },
                values: new object[] { "3aad8202-defd-444e-a11a-564beaef779b", "testtable", true });

            migrationBuilder.InsertData(
                table: "SDProjects",
                column: "Id",
                value: "f6e62472-c9b2-4737-8a8c-52bfb27fbe1e");

            migrationBuilder.InsertData(
                table: "SDColumns",
                columns: new[] { "Id", "DataType", "Discriminator", "Name", "SDDataTableId", "Synchronize" },
                values: new object[] { "8b4cdb44-23ec-476a-8bb8-c8f446b17104", "System.String", "SDColumn", "stringCol", "3aad8202-defd-444e-a11a-564beaef779b", true });

            migrationBuilder.InsertData(
                table: "SDColumns",
                columns: new[] { "Id", "DataType", "Discriminator", "Name", "SDDataTableId", "Synchronize" },
                values: new object[] { "ae3756ad-b272-451f-9dac-2b5514d6ff37", "System.Int32", "SDColumn", "intCol", "3aad8202-defd-444e-a11a-564beaef779b", true });

            migrationBuilder.InsertData(
                table: "SDColumns",
                columns: new[] { "Id", "DataType", "Discriminator", "Name", "SDDataTableId", "Synchronize" },
                values: new object[] { "aab94d61-e949-4b81-b836-7a86a1aea192", "System.DateTime", "SDColumn", "dateTimeCol", "3aad8202-defd-444e-a11a-564beaef779b", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SDStatuses");

            migrationBuilder.DeleteData(
                table: "SDColumns",
                keyColumn: "Id",
                keyValue: "8b4cdb44-23ec-476a-8bb8-c8f446b17104");

            migrationBuilder.DeleteData(
                table: "SDColumns",
                keyColumn: "Id",
                keyValue: "aab94d61-e949-4b81-b836-7a86a1aea192");

            migrationBuilder.DeleteData(
                table: "SDColumns",
                keyColumn: "Id",
                keyValue: "ae3756ad-b272-451f-9dac-2b5514d6ff37");

            migrationBuilder.DeleteData(
                table: "SDProjects",
                keyColumn: "Id",
                keyValue: "f6e62472-c9b2-4737-8a8c-52bfb27fbe1e");

            migrationBuilder.DeleteData(
                table: "SDDataTables",
                keyColumn: "Id",
                keyValue: "3aad8202-defd-444e-a11a-564beaef779b");
        }
    }
}
