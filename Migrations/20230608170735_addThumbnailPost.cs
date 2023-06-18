using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSVAPI1.Migrations
{
    /// <inheritdoc />
    public partial class addThumbnailPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Post");
        }
    }
}
