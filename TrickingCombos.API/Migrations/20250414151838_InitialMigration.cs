using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickingCombos.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stances",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stances", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Transitions",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transitions", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Variations",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variations", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tricks",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DefaultLandingStanceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tricks", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Tricks_Stances_DefaultLandingStanceName",
                        column: x => x.DefaultLandingStanceName,
                        principalTable: "Stances",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StanceTransition",
                columns: table => new
                {
                    StancesName = table.Column<string>(type: "TEXT", nullable: false),
                    TransitionsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanceTransition", x => new { x.StancesName, x.TransitionsName });
                    table.ForeignKey(
                        name: "FK_StanceTransition_Stances_StancesName",
                        column: x => x.StancesName,
                        principalTable: "Stances",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StanceTransition_Transitions_TransitionsName",
                        column: x => x.TransitionsName,
                        principalTable: "Transitions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransitionStances",
                columns: table => new
                {
                    TransitionName = table.Column<string>(type: "TEXT", nullable: false),
                    StanceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionStances", x => new { x.TransitionName, x.StanceName });
                    table.ForeignKey(
                        name: "FK_TransitionStances_Stances_StanceName",
                        column: x => x.StanceName,
                        principalTable: "Stances",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionStances_Transitions_TransitionName",
                        column: x => x.TransitionName,
                        principalTable: "Transitions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StanceVariation",
                columns: table => new
                {
                    StancesName = table.Column<string>(type: "TEXT", nullable: false),
                    VariationsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanceVariation", x => new { x.StancesName, x.VariationsName });
                    table.ForeignKey(
                        name: "FK_StanceVariation_Stances_StancesName",
                        column: x => x.StancesName,
                        principalTable: "Stances",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StanceVariation_Variations_VariationsName",
                        column: x => x.VariationsName,
                        principalTable: "Variations",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariationStances",
                columns: table => new
                {
                    VariationName = table.Column<string>(type: "TEXT", nullable: false),
                    StanceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationStances", x => new { x.VariationName, x.StanceName });
                    table.ForeignKey(
                        name: "FK_VariationStances_Stances_StanceName",
                        column: x => x.StanceName,
                        principalTable: "Stances",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariationStances_Variations_VariationName",
                        column: x => x.VariationName,
                        principalTable: "Variations",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransitionTrick",
                columns: table => new
                {
                    TransitionsName = table.Column<string>(type: "TEXT", nullable: false),
                    TricksName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionTrick", x => new { x.TransitionsName, x.TricksName });
                    table.ForeignKey(
                        name: "FK_TransitionTrick_Transitions_TransitionsName",
                        column: x => x.TransitionsName,
                        principalTable: "Transitions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionTrick_Tricks_TricksName",
                        column: x => x.TricksName,
                        principalTable: "Tricks",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrickTransitions",
                columns: table => new
                {
                    TrickName = table.Column<string>(type: "TEXT", nullable: false),
                    TransitionName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrickTransitions", x => new { x.TrickName, x.TransitionName });
                    table.ForeignKey(
                        name: "FK_TrickTransitions_Transitions_TransitionName",
                        column: x => x.TransitionName,
                        principalTable: "Transitions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrickTransitions_Tricks_TrickName",
                        column: x => x.TrickName,
                        principalTable: "Tricks",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrickVariation",
                columns: table => new
                {
                    TricksName = table.Column<string>(type: "TEXT", nullable: false),
                    VariationsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrickVariation", x => new { x.TricksName, x.VariationsName });
                    table.ForeignKey(
                        name: "FK_TrickVariation_Tricks_TricksName",
                        column: x => x.TricksName,
                        principalTable: "Tricks",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrickVariation_Variations_VariationsName",
                        column: x => x.VariationsName,
                        principalTable: "Variations",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrickVariations",
                columns: table => new
                {
                    TrickName = table.Column<string>(type: "TEXT", nullable: false),
                    VariationName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrickVariations", x => new { x.TrickName, x.VariationName });
                    table.ForeignKey(
                        name: "FK_TrickVariations_Tricks_TrickName",
                        column: x => x.TrickName,
                        principalTable: "Tricks",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrickVariations_Variations_VariationName",
                        column: x => x.VariationName,
                        principalTable: "Variations",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StanceTransition_TransitionsName",
                table: "StanceTransition",
                column: "TransitionsName");

            migrationBuilder.CreateIndex(
                name: "IX_StanceVariation_VariationsName",
                table: "StanceVariation",
                column: "VariationsName");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionStances_StanceName",
                table: "TransitionStances",
                column: "StanceName");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionTrick_TricksName",
                table: "TransitionTrick",
                column: "TricksName");

            migrationBuilder.CreateIndex(
                name: "IX_Tricks_DefaultLandingStanceName",
                table: "Tricks",
                column: "DefaultLandingStanceName");

            migrationBuilder.CreateIndex(
                name: "IX_TrickTransitions_TransitionName",
                table: "TrickTransitions",
                column: "TransitionName");

            migrationBuilder.CreateIndex(
                name: "IX_TrickVariation_VariationsName",
                table: "TrickVariation",
                column: "VariationsName");

            migrationBuilder.CreateIndex(
                name: "IX_TrickVariations_VariationName",
                table: "TrickVariations",
                column: "VariationName");

            migrationBuilder.CreateIndex(
                name: "IX_VariationStances_StanceName",
                table: "VariationStances",
                column: "StanceName");
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
