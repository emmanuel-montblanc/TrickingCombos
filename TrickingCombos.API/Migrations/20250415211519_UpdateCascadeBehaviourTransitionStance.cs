using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrickingCombos.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeBehaviourTransitionStance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StanceId1",
                table: "TransitionStances",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransitionId1",
                table: "TransitionStances",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransitionStances_StanceId1",
                table: "TransitionStances",
                column: "StanceId1");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionStances_TransitionId1",
                table: "TransitionStances",
                column: "TransitionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransitionStances_Stances_StanceId1",
                table: "TransitionStances",
                column: "StanceId1",
                principalTable: "Stances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransitionStances_Transitions_TransitionId1",
                table: "TransitionStances",
                column: "TransitionId1",
                principalTable: "Transitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransitionStances_Stances_StanceId1",
                table: "TransitionStances");

            migrationBuilder.DropForeignKey(
                name: "FK_TransitionStances_Transitions_TransitionId1",
                table: "TransitionStances");

            migrationBuilder.DropIndex(
                name: "IX_TransitionStances_StanceId1",
                table: "TransitionStances");

            migrationBuilder.DropIndex(
                name: "IX_TransitionStances_TransitionId1",
                table: "TransitionStances");

            migrationBuilder.DropColumn(
                name: "StanceId1",
                table: "TransitionStances");

            migrationBuilder.DropColumn(
                name: "TransitionId1",
                table: "TransitionStances");
        }
    }
}
