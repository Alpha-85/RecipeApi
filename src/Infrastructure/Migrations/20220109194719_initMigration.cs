using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApi.Infrastructure.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<long>(type: "bigint", nullable: false),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mealtype = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CollectionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeCollection_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", maxLength: 128, nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeCollectionId = table.Column<int>(type: "int", nullable: false),
                    WeekdayId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeDay_RecipeCollection_RecipeCollectionId",
                        column: x => x.RecipeCollectionId,
                        principalTable: "RecipeCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDay_RecipeInformation_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "RecipeInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDay_WeekDay_WeekdayId",
                        column: x => x.WeekdayId,
                        principalTable: "WeekDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCollection_UserId",
                table: "RecipeCollection",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDay_RecipeCollectionId",
                table: "RecipeDay",
                column: "RecipeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDay_RecipeId",
                table: "RecipeDay",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDay_WeekdayId",
                table: "RecipeDay",
                column: "WeekdayId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeDay");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RecipeCollection");

            migrationBuilder.DropTable(
                name: "RecipeInformation");

            migrationBuilder.DropTable(
                name: "WeekDay");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
