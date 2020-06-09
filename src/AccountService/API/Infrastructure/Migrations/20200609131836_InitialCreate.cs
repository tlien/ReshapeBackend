using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountService.API.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "analysis_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_analysis_profiles", x => x.id);
                });

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
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    street1 = table.Column<string>(nullable: true),
                    street2 = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    zip_code = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    contact_person_full_name = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    business_tier_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_accounts_business_tiers_business_tier_id",
                        column: x => x.business_tier_id,
                        principalTable: "business_tiers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "account_analysis_profile",
                columns: table => new
                {
                    account_id = table.Column<Guid>(nullable: false),
                    analysis_profile_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_analysis_profile", x => new { x.account_id, x.analysis_profile_id });
                    table.ForeignKey(
                        name: "fk_account_analysis_profile_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_account_analysis_profile_analysis_profiles_analysis_profile",
                        column: x => x.analysis_profile_id,
                        principalTable: "analysis_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account_feature",
                columns: table => new
                {
                    account_id = table.Column<Guid>(nullable: false),
                    feature_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account_feature", x => new { x.account_id, x.feature_id });
                    table.ForeignKey(
                        name: "fk_account_feature_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_account_feature_features_feature_id",
                        column: x => x.feature_id,
                        principalTable: "features",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_account_analysis_profile_analysis_profile_id",
                table: "account_analysis_profile",
                column: "analysis_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_account_feature_feature_id",
                table: "account_feature",
                column: "feature_id");

            migrationBuilder.CreateIndex(
                name: "ix_accounts_business_tier_id",
                table: "accounts",
                column: "business_tier_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_analysis_profile");

            migrationBuilder.DropTable(
                name: "account_feature");

            migrationBuilder.DropTable(
                name: "analysis_profiles");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "features");

            migrationBuilder.DropTable(
                name: "business_tiers");
        }
    }
}
