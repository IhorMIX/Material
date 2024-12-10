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

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Users_UserId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_UserId",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorizationInfo",
                table: "AuthorizationInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Materials");

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
                name: "FavoriteListMaterialEntity",
                columns: table => new
                {
                    FavoriteListsId = table.Column<int>(type: "int", nullable: false),
                    MaterialsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteListMaterialEntity", x => new { x.FavoriteListsId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_FavoriteListMaterialEntity_FavoriteLists_FavoriteListsId",
                        column: x => x.FavoriteListsId,
                        principalTable: "FavoriteLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteListMaterialEntity_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteListMaterialEntity_MaterialsId",
                table: "FavoriteListMaterialEntity",
                column: "MaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteLists_UserId",
                table: "FavoriteLists",
                column: "UserId",
                unique: true);

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
                name: "FavoriteListMaterialEntity");

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

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorizationInfo",
                table: "AuthorizationInfo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UserId",
                table: "Materials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorizationInfo_Users_UserId",
                table: "AuthorizationInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Users_UserId",
                table: "Materials",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
