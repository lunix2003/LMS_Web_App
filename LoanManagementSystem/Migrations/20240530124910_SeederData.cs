using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeederData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23fae56d-cd6e-4e68-98fc-ffc0179ddf32", "2", "Loan Auditor", "Loan Auditor" },
                    { "ca401859-f658-4e19-bea9-14f51fb663bb", "3", "Collections Agent", "Collections Agent" },
                    { "d2b68927-47ab-4e66-9a7a-e50af90a913d", "1", "Administrator", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7541a394-02ed-4813-960c-bb0b90ceb4db", 0, "1", "admin@lms.com", false, false, null, "ADMIN@LMS.COM", "ADMIN@LMS.COM", "AQAAAAIAAYagAAAAEBVgkodIXwXZeOtoo2L7f7gibk67s8fukiVgUlVE/3QD2g6GFEFs989SolcutKZDdw==", null, false, "72912718-bff7-4ab0-bb50-ece9a18e9c0f", false, "admin@lms.com" });


            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[]
                { "7541a394-02ed-4813-960c-bb0b90ceb4db","d2b68927-47ab-4e66-9a7a-e50af90a913d" });
                
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23fae56d-cd6e-4e68-98fc-ffc0179ddf32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca401859-f658-4e19-bea9-14f51fb663bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2b68927-47ab-4e66-9a7a-e50af90a913d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7541a394-02ed-4813-960c-bb0b90ceb4db");
        }
    }
}
