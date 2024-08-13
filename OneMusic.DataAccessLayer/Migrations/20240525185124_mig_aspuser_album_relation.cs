using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneMusic.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_aspuser_album_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Albums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AppUserID",
                table: "Albums",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_AppUserID",
                table: "Albums",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_AppUserID",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_AppUserID",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Albums");
        }
    }
}
