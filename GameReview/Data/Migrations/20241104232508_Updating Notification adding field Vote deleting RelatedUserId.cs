using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReview.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingNotificationaddingfieldVotedeletingRelatedUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_RelatedUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_RelatedUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "RelatedUserId",
                table: "Notification");

            migrationBuilder.AddColumn<int>(
                name: "VoteId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_VoteId",
                table: "Notification",
                column: "VoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Votes_VoteId",
                table: "Notification",
                column: "VoteId",
                principalTable: "Votes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Votes_VoteId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_VoteId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "VoteId",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "RelatedUserId",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_RelatedUserId",
                table: "Notification",
                column: "RelatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_RelatedUserId",
                table: "Notification",
                column: "RelatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
