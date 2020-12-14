using Microsoft.EntityFrameworkCore.Migrations;

namespace WzorceGameShop.Migrations
{
    public partial class billgames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketGame_Baskets_BasketId",
                table: "BasketGame");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketGame_Games_GameId",
                table: "BasketGame");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientGame_Clients_ClientId",
                table: "ClientGame");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientGame_Games_GameId",
                table: "ClientGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientGame",
                table: "ClientGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketGame",
                table: "BasketGame");

            migrationBuilder.RenameTable(
                name: "ClientGame",
                newName: "ClientsGames");

            migrationBuilder.RenameTable(
                name: "BasketGame",
                newName: "BasketsGames");

            migrationBuilder.RenameIndex(
                name: "IX_ClientGame_GameId",
                table: "ClientsGames",
                newName: "IX_ClientsGames_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketGame_GameId",
                table: "BasketsGames",
                newName: "IX_BasketsGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientsGames",
                table: "ClientsGames",
                columns: new[] { "ClientId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketsGames",
                table: "BasketsGames",
                columns: new[] { "BasketId", "GameId" });

            migrationBuilder.CreateTable(
                name: "BillsGames",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    BillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillsGames", x => new { x.BillId, x.GameId });
                    table.ForeignKey(
                        name: "FK_BillsGames_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillsGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillsGames_GameId",
                table: "BillsGames",
                column: "GameId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsGames_Clients_ClientId",
                table: "ClientsGames",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsGames_Games_GameId",
                table: "ClientsGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketsGames_Baskets_BasketId",
                table: "BasketsGames");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketsGames_Games_GameId",
                table: "BasketsGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientsGames_Clients_ClientId",
                table: "ClientsGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientsGames_Games_GameId",
                table: "ClientsGames");

            migrationBuilder.DropTable(
                name: "BillsGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientsGames",
                table: "ClientsGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketsGames",
                table: "BasketsGames");

            migrationBuilder.RenameTable(
                name: "ClientsGames",
                newName: "ClientGame");

            migrationBuilder.RenameTable(
                name: "BasketsGames",
                newName: "BasketGame");

            migrationBuilder.RenameIndex(
                name: "IX_ClientsGames_GameId",
                table: "ClientGame",
                newName: "IX_ClientGame_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsGames_GameId",
                table: "BasketGame",
                newName: "IX_BasketGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientGame",
                table: "ClientGame",
                columns: new[] { "ClientId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketGame",
                table: "BasketGame",
                columns: new[] { "BasketId", "GameId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClientGame_Clients_ClientId",
                table: "ClientGame",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientGame_Games_GameId",
                table: "ClientGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
