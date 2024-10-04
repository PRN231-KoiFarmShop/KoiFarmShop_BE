using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ks.infras.Migrations
{
    /// <inheritdoc />
    public partial class add_fishImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "fish",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "fish");
        }
    }
}
