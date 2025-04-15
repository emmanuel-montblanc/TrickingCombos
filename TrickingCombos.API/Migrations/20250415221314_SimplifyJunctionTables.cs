using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickingCombos.API.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyJunctionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StanceVariation");

            migrationBuilder.DropTable(
                name: "TransitionStances");

            migrationBuilder.DropTable(
                name: "TransitionTrick");

            migrationBuilder.DropTable(
                name: "TrickTransitions");

            migrationBuilder.DropTable(
                name: "TrickVariations");

            migrationBuilder.DropTable(
                name: "VariationStances");

            migrationBuilder.AddColumn<Guid>(
                name: "LandingStanceId",
                table: "Variations",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StanceId",
                table: "Variations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrickId",
                table: "Transitions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variations_LandingStanceId",
                table: "Variations",
                column: "LandingStanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Variations_StanceId",
                table: "Variations",
                column: "StanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transitions_TrickId",
                table: "Transitions",
                column: "TrickId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transitions_Tricks_TrickId",
                table: "Transitions",
                column: "TrickId",
                principalTable: "Tricks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Variations_Stances_LandingStanceId",
                table: "Variations",
                column: "LandingStanceId",
                principalTable: "Stances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Variations_Stances_StanceId",
                table: "Variations",
                column: "StanceId",
                principalTable: "Stances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transitions_Tricks_TrickId",
                table: "Transitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Variations_Stances_LandingStanceId",
                table: "Variations");

            migrationBuilder.DropForeignKey(
                name: "FK_Variations_Stances_StanceId",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Variations_LandingStanceId",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Variations_StanceId",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Transitions_TrickId",
                table: "Transitions");

            migrationBuilder.DropColumn(
                name: "LandingStanceId",
                table: "Variations");

            migrationBuilder.DropColumn(
                name: "StanceId",
                table: "Variations");

            migrationBuilder.DropColumn(
                name: "TrickId",
                table: "Transitions");

            migrationBuilder.CreateTable(
                name: "StanceVariation",
                columns: table => new
                {
                    StancesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VariationsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanceVariation", x => new { x.StancesId, x.VariationsId });
                    table.ForeignKey(
                        name: "FK_StanceVariation_Stances_StancesId",
                        column: x => x.StancesId,
                        principalTable: "Stances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StanceVariation_Variations_VariationsId",
                        column: x => x.VariationsId,
                        principalTable: "Variations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransitionStances",
                columns: table => new
                {
                    TransitionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StanceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StanceId1 = table.Column<Guid>(type: "TEXT", nullable: true),
                    TransitionId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionStances", x => new { x.TransitionId, x.StanceId });
                    table.ForeignKey(
                        name: "FK_TransitionStances_Stances_StanceId",
                        column: x => x.StanceId,
                        principalTable: "Stances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionStances_Stances_StanceId1",
                        column: x => x.StanceId1,
                        principalTable: "Stances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransitionStances_Transitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "Transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionStances_Transitions_TransitionId1",
                        column: x => x.TransitionId1,
                        principalTable: "Transitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransitionTrick",
                columns: table => new
                {
                    TransitionsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TricksId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionTrick", x => new { x.TransitionsId, x.TricksId });
                    table.ForeignKey(
                        name: "FK_TransitionTrick_Transitions_TransitionsId",
                        column: x => x.TransitionsId,
                        principalTable: "Transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionTrick_Tricks_TricksId",
                        column: x => x.TricksId,
                        principalTable: "Tricks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrickTransitions",
                columns: table => new
                {
                    TrickId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransitionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrickTransitions", x => new { x.TrickId, x.TransitionId });
                    table.ForeignKey(
                        name: "FK_TrickTransitions_Transitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "Transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrickTransitions_Tricks_TrickId",
                        column: x => x.TrickId,
                        principalTable: "Tricks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrickVariations",
                columns: table => new
                {
                    TrickId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VariationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrickVariations", x => new { x.TrickId, x.VariationId });
                    table.ForeignKey(
                        name: "FK_TrickVariations_Tricks_TrickId",
                        column: x => x.TrickId,
                        principalTable: "Tricks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrickVariations_Variations_VariationId",
                        column: x => x.VariationId,
                        principalTable: "Variations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariationStances",
                columns: table => new
                {
                    VariationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StanceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationStances", x => new { x.VariationId, x.StanceId });
                    table.ForeignKey(
                        name: "FK_VariationStances_Stances_StanceId",
                        column: x => x.StanceId,
                        principalTable: "Stances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariationStances_Variations_VariationId",
                        column: x => x.VariationId,
                        principalTable: "Variations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StanceVariation_VariationsId",
                table: "StanceVariation",
                column: "VariationsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionStances_StanceId",
                table: "TransitionStances",
                column: "StanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionStances_StanceId1",
                table: "TransitionStances",
                column: "StanceId1");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionStances_TransitionId1",
                table: "TransitionStances",
                column: "TransitionId1");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionTrick_TricksId",
                table: "TransitionTrick",
                column: "TricksId");

            migrationBuilder.CreateIndex(
                name: "IX_TrickTransitions_TransitionId",
                table: "TrickTransitions",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrickVariations_VariationId",
                table: "TrickVariations",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationStances_StanceId",
                table: "VariationStances",
                column: "StanceId");
        }
    }
}
