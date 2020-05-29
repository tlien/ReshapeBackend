using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountService.API.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.CreateTable(
                name: "businesstiers",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businesstiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address_Street1 = table.Column<string>(nullable: true),
                    Address_Street2 = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_ZipCode = table.Column<string>(nullable: true),
                    Address_Country = table.Column<string>(nullable: true),
                    ContactDetails_ContactPersonFullName = table.Column<string>(nullable: true),
                    ContactDetails_Phone = table.Column<string>(nullable: true),
                    ContactDetails_Email = table.Column<string>(nullable: true),
                    BusinessTierId = table.Column<Guid>(nullable: true),
                    isactive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accounts_businesstiers_BusinessTierId",
                        column: x => x.BusinessTierId,
                        principalSchema: "account",
                        principalTable: "businesstiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "analysisProfiles",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analysisProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_analysisProfiles_accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "account",
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "features",
                schema: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_features_accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "account",
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "accountanalysisProfile",
                schema: "account",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    AnalysisProfileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accountanalysisProfile", x => new { x.AccountId, x.AnalysisProfileId });
                    table.ForeignKey(
                        name: "FK_accountanalysisProfile_accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "account",
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accountanalysisProfile_analysisProfiles_AnalysisProfileId",
                        column: x => x.AnalysisProfileId,
                        principalSchema: "account",
                        principalTable: "analysisProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accountfeatures",
                schema: "account",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    FeatureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accountfeatures", x => new { x.AccountId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_accountfeatures_accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "account",
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accountfeatures_features_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "account",
                        principalTable: "features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accountanalysisProfile_AnalysisProfileId",
                schema: "account",
                table: "accountanalysisProfile",
                column: "AnalysisProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_accountfeatures_FeatureId",
                schema: "account",
                table: "accountfeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_BusinessTierId",
                schema: "account",
                table: "accounts",
                column: "BusinessTierId");

            migrationBuilder.CreateIndex(
                name: "IX_analysisProfiles_AccountId",
                schema: "account",
                table: "analysisProfiles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_features_AccountId",
                schema: "account",
                table: "features",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accountanalysisProfile",
                schema: "account");

            migrationBuilder.DropTable(
                name: "accountfeatures",
                schema: "account");

            migrationBuilder.DropTable(
                name: "analysisProfiles",
                schema: "account");

            migrationBuilder.DropTable(
                name: "features",
                schema: "account");

            migrationBuilder.DropTable(
                name: "accounts",
                schema: "account");

            migrationBuilder.DropTable(
                name: "businesstiers",
                schema: "account");
        }
    }
}
