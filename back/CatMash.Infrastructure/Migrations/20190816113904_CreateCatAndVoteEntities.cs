using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatMash.Infrastructure.Migrations
{
    public partial class CreateCatAndVoteEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Occurence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoteCat",
                columns: table => new
                {
                    CatId = table.Column<string>(nullable: false),
                    VoteId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteCat", x => new { x.CatId, x.VoteId });
                    table.ForeignKey(
                        name: "FK_VoteCat_Cat_CatId",
                        column: x => x.CatId,
                        principalTable: "Cat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteCat_Vote_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoteCat_VoteId",
                table: "VoteCat",
                column: "VoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteCat");

            migrationBuilder.DropTable(
                name: "Cat");

            migrationBuilder.DropTable(
                name: "Vote");
        }
    }
}
