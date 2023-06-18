using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSVAPI1.Migrations
{
    /// <inheritdoc />
    public partial class addColumnLevelComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Comment");
        }
    }
}
