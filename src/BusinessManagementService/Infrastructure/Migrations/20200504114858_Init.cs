using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessManagementService.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisProfileRequiredFeatures",
                columns: table => new
                {
                    AnalysisProfileID = table.Column<Guid>(nullable: false),
                    FeatureID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisProfileRequiredFeatures", x => new { x.AnalysisProfileID, x.FeatureID });
                    table.ForeignKey(
                        name: "FK_AnalysisProfileRequiredFeatures_AnalysisProfiles_AnalysisPr~",
                        column: x => x.AnalysisProfileID,
                        principalTable: "AnalysisProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalysisProfileRequiredFeatures_Features_FeatureID",
                        column: x => x.FeatureID,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisProfileRequiredFeatures_FeatureID",
                table: "AnalysisProfileRequiredFeatures",
                column: "FeatureID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisProfileRequiredFeatures");

            migrationBuilder.DropTable(
                name: "BusinessTiers");

            migrationBuilder.DropTable(
                name: "AnalysisProfiles");

            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
