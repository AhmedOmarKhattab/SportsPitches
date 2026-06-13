using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveStadium.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pitches_PitchTypes_ProductTypeId",
                table: "Pitches");

            migrationBuilder.DropIndex(
                name: "IX_Pitches_ProductTypeId",
                table: "Pitches");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Pitches");

            migrationBuilder.CreateIndex(
                name: "IX_Pitches_PitchTypeId",
                table: "Pitches",
                column: "PitchTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pitches_PitchTypes_PitchTypeId",
                table: "Pitches",
                column: "PitchTypeId",
                principalTable: "PitchTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pitches_PitchTypes_PitchTypeId",
                table: "Pitches");

            migrationBuilder.DropIndex(
                name: "IX_Pitches_PitchTypeId",
                table: "Pitches");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Pitches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pitches_ProductTypeId",
                table: "Pitches",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pitches_PitchTypes_ProductTypeId",
                table: "Pitches",
                column: "ProductTypeId",
                principalTable: "PitchTypes",
                principalColumn: "Id");
        }
    }
}
