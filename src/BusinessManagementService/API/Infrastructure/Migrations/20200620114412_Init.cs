using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessManagementService.API.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "business_tiers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_tiers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "features",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_features", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media_types",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "script_files",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    script = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_script_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "script_parameters_files",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    script_parameters = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_script_parameters_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "analysis_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    media_type_id = table.Column<Guid>(nullable: true),
                    script_file_id = table.Column<Guid>(nullable: true),
                    script_parameters_file_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_analysis_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_analysis_profiles_media_types_media_type_id",
                        column: x => x.media_type_id,
                        principalTable: "media_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_analysis_profiles_script_files_script_file_id",
                        column: x => x.script_file_id,
                        principalTable: "script_files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_analysis_profiles_script_parameters_files_script_parameters",
                        column: x => x.script_parameters_file_id,
                        principalTable: "script_parameters_files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_analysis_profiles_media_type_id",
                table: "analysis_profiles",
                column: "media_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_analysis_profiles_script_file_id",
                table: "analysis_profiles",
                column: "script_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_analysis_profiles_script_parameters_file_id",
                table: "analysis_profiles",
                column: "script_parameters_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_media_types_name",
                table: "media_types",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analysis_profiles");

            migrationBuilder.DropTable(
                name: "business_tiers");

            migrationBuilder.DropTable(
                name: "features");

            migrationBuilder.DropTable(
                name: "media_types");

            migrationBuilder.DropTable(
                name: "script_files");

            migrationBuilder.DropTable(
                name: "script_parameters_files");
        }
    }
}
