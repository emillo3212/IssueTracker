using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ProjectUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_Projects_ProjectsId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_Users_UsersId",
                table: "ProjectUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ProjectUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "ProjectUser",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUser_UsersId",
                table: "ProjectUser",
                newName: "IX_ProjectUser_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_Projects_ProjectId",
                table: "ProjectUser",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_Users_UserId",
                table: "ProjectUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_Projects_ProjectId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_Users_UserId",
                table: "ProjectUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ProjectUser",
                newName: "ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUser_UserId",
                table: "ProjectUser",
                newName: "IX_ProjectUser_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_Projects_ProjectsId",
                table: "ProjectUser",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_Users_UsersId",
                table: "ProjectUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
