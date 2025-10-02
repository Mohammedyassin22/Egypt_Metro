using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistense.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Line_Names",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line_Names", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Station_Names",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station_Names", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket_Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationsNumber = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chatbots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TriggerPhrase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatbots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faults_Line_Names_LineId",
                        column: x => x.LineId,
                        principalTable: "Line_Names",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faults_Station_Names_StationId",
                        column: x => x.StationId,
                        principalTable: "Station_Names",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Station_Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    Line_NameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station_Coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Station_Coordinates_Line_Names_Line_NameId",
                        column: x => x.Line_NameId,
                        principalTable: "Line_Names",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Station_Coordinates_Station_Names_StationId",
                        column: x => x.StationId,
                        principalTable: "Station_Names",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stations_Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    OrderInLine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations_Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Lines_Line_Names_LineId",
                        column: x => x.LineId,
                        principalTable: "Line_Names",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stations_Lines_Station_Names_StationId",
                        column: x => x.StationId,
                        principalTable: "Station_Names",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rush_Times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    ObservationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    congestionLevel = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rush_Times", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rush_Times_Stations_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Stations_Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faults_LineId",
                table: "Faults",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Faults_StationId",
                table: "Faults",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rush_Times_LineId",
                table: "Rush_Times",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_Coordinates_Line_NameId",
                table: "Station_Coordinates",
                column: "Line_NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_Coordinates_StationId",
                table: "Station_Coordinates",
                column: "StationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_Lines_LineId",
                table: "Stations_Lines",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_Lines_StationId",
                table: "Stations_Lines",
                column: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faults");

            migrationBuilder.DropTable(
                name: "Rush_Times");

            migrationBuilder.DropTable(
                name: "Station_Coordinates");

            migrationBuilder.DropTable(
                name: "Ticket_Prices");

            migrationBuilder.DropTable(
                name: "chatbots");

            migrationBuilder.DropTable(
                name: "Stations_Lines");

            migrationBuilder.DropTable(
                name: "Line_Names");

            migrationBuilder.DropTable(
                name: "Station_Names");
        }
    }
}
