using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakweneTrackManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnTrackIsrc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isrc",
                table: "Tracks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_Isrc",
                table: "Tracks",
                column: "Isrc",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tracks_Isrc",
                table: "Tracks");

            migrationBuilder.AlterColumn<string>(
                name: "Isrc",
                table: "Tracks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
