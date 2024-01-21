using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tech_task.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItems_ConfigurationItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ConfigurationItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JSONFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RootElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JSONFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JSONNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    JSONFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JSONNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JSONNodes_JSONFiles_JSONFileId",
                        column: x => x.JSONFileId,
                        principalTable: "JSONFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JSONNodes_JSONNodes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JSONNodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JSONLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JSONLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JSONLeaves_JSONNodes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JSONNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItems_ParentId",
                table: "ConfigurationItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JSONLeaves_ParentId",
                table: "JSONLeaves",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JSONNodes_JSONFileId",
                table: "JSONNodes",
                column: "JSONFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JSONNodes_ParentId",
                table: "JSONNodes",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationItems");

            migrationBuilder.DropTable(
                name: "JSONLeaves");

            migrationBuilder.DropTable(
                name: "JSONNodes");

            migrationBuilder.DropTable(
                name: "JSONFiles");
        }
    }
}
