using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tech_task.Migrations
{
    /// <inheritdoc />
    public partial class testing1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JSONLeaves");

            migrationBuilder.DropTable(
                name: "JSONNodes");

            migrationBuilder.DropTable(
                name: "JSONFiles");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ConfigurationItems",
                newName: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "ConfigurationItems",
                newName: "Name");

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
                    JSONFileId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
