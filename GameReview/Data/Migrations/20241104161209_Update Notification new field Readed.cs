using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReview.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotificationnewfieldReaded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Readed",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Readed",
                table: "Notification");
        }
    }
}
