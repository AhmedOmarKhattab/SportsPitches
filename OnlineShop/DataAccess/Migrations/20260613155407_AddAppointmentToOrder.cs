using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveStadium.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PitchAppointmentId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_PitchAppointmentId",
                table: "orders",
                column: "PitchAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Appointments_PitchAppointmentId",
                table: "orders",
                column: "PitchAppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_Appointments_PitchAppointmentId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_PitchAppointmentId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "PitchAppointmentId",
                table: "orders");
        }
    }
}
