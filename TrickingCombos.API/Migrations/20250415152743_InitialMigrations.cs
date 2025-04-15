using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickingCombos.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tricks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DefaultLandingStanceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tricks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tricks_Stances_DefaultLandingStanceId",
                        column: x => x.DefaultLandingStanceId,
                        principalTable: "Stances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StanceTransition",
                columns: table => new
                {
                    StancesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransitionsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanceTransition", x => new { x.StancesId, x.TransitionsId });
                    table.ForeignKey(
                        name: "FK_StanceTransition_Stances_StancesId",
                        column: x => x.StancesId,
                        principalTable: "Stances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StanceTransition_Transitions_TransitionsId",
                        column: x => x.TransitionsId,
                        principalTable: "Transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransitionStances",
                columns: table => new
                {
                    TransitionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StanceId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                        name: "FK_TransitionStances_Transitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "Transitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "TrickVariation",
                columns: table => new
                {
                    TricksId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VariationsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrickVariation", x => new { x.TricksId, x.VariationsId });
                    table.ForeignKey(
                        name: "FK_TrickVariation_Tricks_TricksId",
                        column: x => x.TricksId,
                        principalTable: "Tricks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrickVariation_Variations_VariationsId",
                        column: x => x.VariationsId,
                        principalTable: "Variations",
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

            migrationBuilder.CreateIndex(
                name: "IX_StanceTransition_TransitionsId",
                table: "StanceTransition",
                column: "TransitionsId");

            migrationBuilder.CreateIndex(
                name: "IX_StanceVariation_VariationsId",
                table: "StanceVariation",
                column: "VariationsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionStances_StanceId",
                table: "TransitionStances",
                column: "StanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionTrick_TricksId",
                table: "TransitionTrick",
                column: "TricksId");

            migrationBuilder.CreateIndex(
                name: "IX_Tricks_DefaultLandingStanceId",
                table: "Tricks",
                column: "DefaultLandingStanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TrickTransitions_TransitionId",
                table: "TrickTransitions",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrickVariation_VariationsId",
                table: "TrickVariation",
                column: "VariationsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrickVariations_VariationId",
                table: "TrickVariations",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationStances_StanceId",
                table: "VariationStances",
                column: "StanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StanceTransition");

            migrationBuilder.DropTable(
                name: "StanceVariation");

            migrationBuilder.DropTable(
                name: "TransitionStances");

            migrationBuilder.DropTable(
                name: "TransitionTrick");

            migrationBuilder.DropTable(
                name: "TrickTransitions");

            migrationBuilder.DropTable(
                name: "TrickVariation");

            migrationBuilder.DropTable(
                name: "TrickVariations");

            migrationBuilder.DropTable(
                name: "VariationStances");

            migrationBuilder.DropTable(
                name: "Transitions");

            migrationBuilder.DropTable(
                name: "Tricks");

            migrationBuilder.DropTable(
                name: "Variations");

            migrationBuilder.DropTable(
                name: "Stances");
        }
    }
}
