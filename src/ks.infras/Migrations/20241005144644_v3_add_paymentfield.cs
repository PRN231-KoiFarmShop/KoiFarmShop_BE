using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ks.infras.Migrations
{
    /// <inheritdoc />
    public partial class v3_add_paymentfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_order_OrderId",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_News",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "news");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_OrderId",
                table: "feedback",
                newName: "IX_feedback_OrderId");

            migrationBuilder.AddColumn<string>(
                name: "TransactionNo",
                table: "payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TxnRef",
                table: "payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_news",
                table: "news",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_feedback",
                table: "feedback",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_order_OrderId",
                table: "feedback",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_order_OrderId",
                table: "feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_news",
                table: "news");

            migrationBuilder.DropPrimaryKey(
                name: "PK_feedback",
                table: "feedback");

            migrationBuilder.DropColumn(
                name: "TransactionNo",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "TxnRef",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "order");

            migrationBuilder.RenameTable(
                name: "news",
                newName: "News");

            migrationBuilder.RenameTable(
                name: "feedback",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_feedback_OrderId",
                table: "Feedback",
                newName: "IX_Feedback_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_News",
                table: "News",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_order_OrderId",
                table: "Feedback",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
