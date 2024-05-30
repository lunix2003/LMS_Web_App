using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class modify_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserPermissions");

            migrationBuilder.DropTable(
                name: "AppUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.AppUserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserPermissions",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    UserPermission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserPermissions", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_AppUserPermissions_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
