using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace retailApp.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsQualified = table.Column<bool>(type: "bit", nullable: false),
                    DisqualificationReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "DisqualificationReasons",
                columns: table => new
                {
                    DisqualificationReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisqualificationReasons", x => x.DisqualificationReasonId);
                    table.ForeignKey(
                        name: "FK_DisqualificationReasons_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisqualificationReasons_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisqualificationReasons_ProductId",
                table: "DisqualificationReasons",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisqualificationReasons_ProductId1",
                table: "DisqualificationReasons",
                column: "ProductId1",
                unique: true,
                filter: "[ProductId1] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisqualificationReasons");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
