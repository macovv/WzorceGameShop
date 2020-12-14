using Microsoft.EntityFrameworkCore.Migrations;

namespace WzorceGameShop.Migrations
{
    public partial class clientgame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketsGames_Baskets_BasketId",
                table: "BasketsGames");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketsGames_Games_GameId",
                table: "BasketsGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketsGames",
                table: "BasketsGames");

            migrationBuilder.RenameTable(
                name: "BasketsGames",
                newName: "BasketGame");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsGames_GameId",
                table: "BasketGame",
                newName: "IX_BasketGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketGame",
                table: "BasketGame",
                columns: new[] { "BasketId", "GameId" });

            migrationBuilder.CreateTable(
                name: "ClientGame",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGame", x => new { x.ClientId, x.GameId });
                    table.ForeignKey(
                        name: "FK_ClientGame_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientGame_GameId",
                table: "ClientGame",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketGame_Baskets_BasketId",
                table: "BasketGame",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketGame_Games_GameId",
                table: "BasketGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketGame_Baskets_BasketId",
                table: "BasketGame");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketGame_Games_GameId",
                table: "BasketGame");

            migrationBuilder.DropTable(
                name: "ClientGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketGame",
                table: "BasketGame");

            migrationBuilder.RenameTable(
                name: "BasketGame",
                newName: "BasketsGames");

            migrationBuilder.RenameIndex(
                name: "IX_BasketGame_GameId",
                table: "BasketsGames",
                newName: "IX_BasketsGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketsGames",
                table: "BasketsGames",
                columns: new[] { "BasketId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketsGames_Baskets_BasketId",
                table: "BasketsGames",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketsGames_Games_GameId",
                table: "BasketsGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
