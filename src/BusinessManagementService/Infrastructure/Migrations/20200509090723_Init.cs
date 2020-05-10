using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessManagementService.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "analysisprofiles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    filename = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analysisprofiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "businesstiers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true)
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
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_features", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "analysisprofilerequiredfeatures",
                columns: table => new
                {
                    analysisprofileid = table.Column<Guid>(nullable: false),
                    featureid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analysisprofilerequiredfeatures", x => new { x.analysisprofileid, x.featureid });
                    table.ForeignKey(
                        name: "FK_analysisprofilerequiredfeatures_analysisprofiles_analysispr~",
                        column: x => x.analysisprofileid,
                        principalTable: "analysisprofiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_analysisprofilerequiredfeatures_features_featureid",
                        column: x => x.featureid,
                        principalTable: "features",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_analysisprofilerequiredfeatures_featureid",
                table: "analysisprofilerequiredfeatures",
                column: "featureid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analysisprofilerequiredfeatures");

            migrationBuilder.DropTable(
                name: "businesstiers");

            migrationBuilder.DropTable(
                name: "analysisprofiles");

            migrationBuilder.DropTable(
                name: "features");
        }
    }
}
