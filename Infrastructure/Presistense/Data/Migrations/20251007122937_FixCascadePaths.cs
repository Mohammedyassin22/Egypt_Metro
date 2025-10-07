using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistense.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faults_Line_Names_LineId",
                table: "Faults");

            migrationBuilder.DropForeignKey(
                name: "FK_Faults_Station_Names_StationId",
                table: "Faults");

            migrationBuilder.DropForeignKey(
                name: "FK_Rush_Times_Stations_Lines_LineId",
                table: "Rush_Times");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_Coordinates_Line_Names_Line_NameId",
                table: "Station_Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_Coordinates_Station_Names_StationId",
                table: "Station_Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Lines_Line_Names_LineId",
                table: "Stations_Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Lines_Station_Names_StationId",
                table: "Stations_Lines");

            migrationBuilder.DropIndex(
                name: "IX_Faults_StationId",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Faults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Faults");

            migrationBuilder.RenameColumn(
                name: "OrderInLine",
                table: "Stations_Lines",
                newName: "StationNameId");

            migrationBuilder.AddColumn<string>(
                name: "LineName",
                table: "Stations_Lines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Station_NameId",
                table: "Rush_Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CongestionSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationNameId = table.Column<int>(type: "int", nullable: false),
                    NameId = table.Column<int>(type: "int", nullable: false),
                    ObservationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    congestionLevel = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongestionSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CongestionSchedules_Station_Names_NameId",
                        column: x => x.NameId,
                        principalTable: "Station_Names",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rush_Times_Station_NameId",
                table: "Rush_Times",
                column: "Station_NameId");

            migrationBuilder.CreateIndex(
                name: "IX_CongestionSchedules_NameId",
                table: "CongestionSchedules",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faults_Line_Names_LineId",
                table: "Faults",
                column: "LineId",
                principalTable: "Line_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rush_Times_Station_Names_Station_NameId",
                table: "Rush_Times",
                column: "Station_NameId",
                principalTable: "Station_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rush_Times_Stations_Lines_LineId",
                table: "Rush_Times",
                column: "LineId",
                principalTable: "Stations_Lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Coordinates_Line_Names_Line_NameId",
                table: "Station_Coordinates",
                column: "Line_NameId",
                principalTable: "Line_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Coordinates_Station_Names_StationId",
                table: "Station_Coordinates",
                column: "StationId",
                principalTable: "Station_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Lines_Line_Names_LineId",
                table: "Stations_Lines",
                column: "LineId",
                principalTable: "Line_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Lines_Station_Names_StationId",
                table: "Stations_Lines",
                column: "StationId",
                principalTable: "Station_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faults_Line_Names_LineId",
                table: "Faults");

            migrationBuilder.DropForeignKey(
                name: "FK_Rush_Times_Station_Names_Station_NameId",
                table: "Rush_Times");

            migrationBuilder.DropForeignKey(
                name: "FK_Rush_Times_Stations_Lines_LineId",
                table: "Rush_Times");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_Coordinates_Line_Names_Line_NameId",
                table: "Station_Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Station_Coordinates_Station_Names_StationId",
                table: "Station_Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Lines_Line_Names_LineId",
                table: "Stations_Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Stations_Lines_Station_Names_StationId",
                table: "Stations_Lines");

            migrationBuilder.DropTable(
                name: "CongestionSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Rush_Times_Station_NameId",
                table: "Rush_Times");

            migrationBuilder.DropColumn(
                name: "LineName",
                table: "Stations_Lines");

            migrationBuilder.DropColumn(
                name: "Station_NameId",
                table: "Rush_Times");

            migrationBuilder.RenameColumn(
                name: "StationNameId",
                table: "Stations_Lines",
                newName: "OrderInLine");

            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "Faults",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Faults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_StationId",
                table: "Faults",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faults_Line_Names_LineId",
                table: "Faults",
                column: "LineId",
                principalTable: "Line_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faults_Station_Names_StationId",
                table: "Faults",
                column: "StationId",
                principalTable: "Station_Names",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rush_Times_Stations_Lines_LineId",
                table: "Rush_Times",
                column: "LineId",
                principalTable: "Stations_Lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Coordinates_Line_Names_Line_NameId",
                table: "Station_Coordinates",
                column: "Line_NameId",
                principalTable: "Line_Names",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Coordinates_Station_Names_StationId",
                table: "Station_Coordinates",
                column: "StationId",
                principalTable: "Station_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Lines_Line_Names_LineId",
                table: "Stations_Lines",
                column: "LineId",
                principalTable: "Line_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_Lines_Station_Names_StationId",
                table: "Stations_Lines",
                column: "StationId",
                principalTable: "Station_Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
