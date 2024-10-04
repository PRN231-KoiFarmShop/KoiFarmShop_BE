using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ks.infras.Migrations
{
    /// <inheritdoc />
    public partial class v2_add_user_salt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "user",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "user");
        }
    }
}
