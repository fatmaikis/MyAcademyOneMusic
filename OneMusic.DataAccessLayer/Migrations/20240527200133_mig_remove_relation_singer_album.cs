using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneMusic.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_remove_relation_singer_album : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Singers_SingerID",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_SingerID",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "SingerID",
                table: "Albums");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SingerID",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_SingerID",
                table: "Albums",
                column: "SingerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Singers_SingerID",
                table: "Albums",
                column: "SingerID",
                principalTable: "Singers",
                principalColumn: "SingerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
