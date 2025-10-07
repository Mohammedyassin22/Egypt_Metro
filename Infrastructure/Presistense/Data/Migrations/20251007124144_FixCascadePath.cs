using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistense.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CongestionSchedules_Station_Names_NameId",
                table: "CongestionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_CongestionSchedules_NameId",
                table: "CongestionSchedules");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "CongestionSchedules");

            migrationBuilder.RenameColumn(
                name: "congestionLevel",
                table: "CongestionSchedules",
                newName: "CongestionLevel");

            migrationBuilder.CreateIndex(
                name: "IX_CongestionSchedules_StationNameId",
                table: "CongestionSchedules",
                column: "StationNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CongestionSchedules_Station_Names_StationNameId",
                table: "CongestionSchedules",
                column: "StationNameId",
                principalTable: "Station_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CongestionSchedules_Station_Names_StationNameId",
                table: "CongestionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_CongestionSchedules_StationNameId",
                table: "CongestionSchedules");

            migrationBuilder.RenameColumn(
                name: "CongestionLevel",
                table: "CongestionSchedules",
                newName: "congestionLevel");

            migrationBuilder.AddColumn<int>(
                name: "NameId",
                table: "CongestionSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CongestionSchedules_NameId",
                table: "CongestionSchedules",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CongestionSchedules_Station_Names_NameId",
                table: "CongestionSchedules",
                column: "NameId",
                principalTable: "Station_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
