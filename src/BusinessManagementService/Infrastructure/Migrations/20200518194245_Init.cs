using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessManagementService.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "businesstiers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businesstiers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "features",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_features", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mediatypes",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mediatypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scriptfiles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    script = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scriptfiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scriptparametersfiles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    scriptparameters = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scriptparametersfiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "analysisprofiles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    mediatypeid = table.Column<Guid>(nullable: false),
                    scriptfileid = table.Column<Guid>(nullable: false),
                    scriptparametersfileid = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analysisprofiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_analysisprofiles_mediatypes_mediatypeid",
                        column: x => x.mediatypeid,
                        principalTable: "mediatypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_analysisprofiles_scriptfiles_scriptfileid",
                        column: x => x.scriptfileid,
                        principalTable: "scriptfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_analysisprofiles_scriptparametersfiles_scriptparametersfile~",
                        column: x => x.scriptparametersfileid,
                        principalTable: "scriptparametersfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_analysisprofiles_mediatypeid",
                table: "analysisprofiles",
                column: "mediatypeid");

            migrationBuilder.CreateIndex(
                name: "IX_analysisprofiles_scriptfileid",
                table: "analysisprofiles",
                column: "scriptfileid");

            migrationBuilder.CreateIndex(
                name: "IX_analysisprofiles_scriptparametersfileid",
                table: "analysisprofiles",
                column: "scriptparametersfileid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analysisprofiles");

            migrationBuilder.DropTable(
                name: "businesstiers");

            migrationBuilder.DropTable(
                name: "features");

            migrationBuilder.DropTable(
                name: "mediatypes");

            migrationBuilder.DropTable(
                name: "scriptfiles");

            migrationBuilder.DropTable(
                name: "scriptparametersfiles");
        }
    }
}
