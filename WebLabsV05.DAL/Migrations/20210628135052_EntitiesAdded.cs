using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLabsV05.DAL.Migrations
{
    public partial class EntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PCPartGroups",
                columns: table => new
                {
                    PCPartGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCPartGroups", x => x.PCPartGroupId);
                });

            migrationBuilder.CreateTable(
                name: "PCParts",
                columns: table => new
                {
                    PCPartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PCPartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCPartGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCParts", x => x.PCPartId);
                    table.ForeignKey(
                        name: "FK_PCParts_PCPartGroups_PCPartGroupId",
                        column: x => x.PCPartGroupId,
                        principalTable: "PCPartGroups",
                        principalColumn: "PCPartGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PCParts_PCPartGroupId",
                table: "PCParts",
                column: "PCPartGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PCParts");

            migrationBuilder.DropTable(
                name: "PCPartGroups");
        }
    }
}
