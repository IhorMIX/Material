using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Material.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration_FavoriteList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizationInfo_Users_UserId",
                table: "AuthorizationInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorizationInfo",
                table: "AuthorizationInfo");

            migrationBuilder.RenameTable(
                name: "AuthorizationInfo",
                newName: "AuthorizationInfos");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorizationInfo_UserId",
                table: "AuthorizationInfos",
                newName: "IX_AuthorizationInfos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorizationInfos",
                table: "AuthorizationInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FavoriteLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoriteListId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteMaterials_FavoriteLists_FavoriteListId",
                        column: x => x.FavoriteListId,
                        principalTable: "FavoriteLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteLists_UserId",
                table: "FavoriteLists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMaterials_FavoriteListId",
                table: "FavoriteMaterials",
                column: "FavoriteListId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMaterials_MaterialId",
                table: "FavoriteMaterials",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationInfos_Users_UserId",
                table: "AuthorizationInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorizationInfos_Users_UserId",
                table: "AuthorizationInfos");

            migrationBuilder.DropTable(
                name: "FavoriteMaterials");

            migrationBuilder.DropTable(
                name: "FavoriteLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorizationInfos",
                table: "AuthorizationInfos");

            migrationBuilder.RenameTable(
                name: "AuthorizationInfos",
                newName: "AuthorizationInfo");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorizationInfos_UserId",
                table: "AuthorizationInfo",
                newName: "IX_AuthorizationInfo_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorizationInfo",
                table: "AuthorizationInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationInfo_Users_UserId",
                table: "AuthorizationInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
