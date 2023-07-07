using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetAuthApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSet",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSet", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSet",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    NextCommit = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastCommit = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSet", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_ExerciseSet_UserSet_UserId",
                        column: x => x.UserId,
                        principalTable: "UserSet",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyRepSet",
                columns: table => new
                {
                    RepId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Repetitions = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRepSet", x => x.RepId);
                    table.ForeignKey(
                        name: "FK_DailyRepSet_ExerciseSet_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "ExerciseSet",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyRepSet_ExerciseId",
                table: "DailyRepSet",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSet_UserId",
                table: "ExerciseSet",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyRepSet");

            migrationBuilder.DropTable(
                name: "ExerciseSet");

            migrationBuilder.DropTable(
                name: "UserSet");
        }
    }
}
