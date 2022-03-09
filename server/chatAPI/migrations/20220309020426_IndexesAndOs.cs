using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatAPI.migrations
{
    public partial class IndexesAndOs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_NormalizedStatusName",
                table: "Statuses");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_NormalizedStatusName",
                table: "Statuses",
                column: "NormalizedStatusName",
                unique: true,
                filter: "[NormalizedStatusName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true,
                filter: "[RoleName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_NormalizedStatusName",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleName",
                table: "Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_NormalizedStatusName",
                table: "Statuses",
                column: "NormalizedStatusName");
        }
    }
}
