using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.AppUserId);
                });

            migrationBuilder.CreateTable(
                name: "collateralType",
                columns: table => new
                {
                    CollateralTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollateralTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collateralType", x => x.CollateralTypeId);
                });

            migrationBuilder.CreateTable(
                name: "creditOfficer",
                columns: table => new
                {
                    CreditOfficerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<int>(type: "int", nullable: false),
                    CreditOfficerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POB = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditOfficer", x => x.CreditOfficerId);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POB = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "appUserPermissions",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    UserPermission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appUserPermissions", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_appUserPermissions_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "collateral",
                columns: table => new
                {
                    CollateralId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<int>(type: "int", nullable: false),
                    CollateralCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerNationalCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CollateralTypeId = table.Column<int>(type: "int", nullable: false),
                    CollateralDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collateral", x => x.CollateralId);
                    table.ForeignKey(
                        name: "FK_collateral_collateralType_CollateralTypeId",
                        column: x => x.CollateralTypeId,
                        principalTable: "collateralType",
                        principalColumn: "CollateralTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AddressName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loan",
                columns: table => new
                {
                    LoadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CollateralId = table.Column<int>(type: "int", nullable: false),
                    CreditOfficerId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoanAmount = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    InterestRate = table.Column<int>(type: "int", nullable: false),
                    PaymentFrequencyCode = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan", x => x.LoadId);
                    table.ForeignKey(
                        name: "FK_loan_collateral_CollateralId",
                        column: x => x.CollateralId,
                        principalTable: "collateral",
                        principalColumn: "CollateralId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_loan_creditOfficer_CreditOfficerId",
                        column: x => x.CreditOfficerId,
                        principalTable: "creditOfficer",
                        principalColumn: "CreditOfficerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_loan_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loadDetails",
                columns: table => new
                {
                    LoanDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadId = table.Column<int>(type: "int", nullable: false),
                    PeriodNo = table.Column<int>(type: "int", nullable: false),
                    BeginningBalance = table.Column<int>(type: "int", nullable: false),
                    Priciple = table.Column<int>(type: "int", nullable: false),
                    Interest = table.Column<int>(type: "int", nullable: false),
                    Payment = table.Column<int>(type: "int", nullable: false),
                    EndingBalance = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<int>(type: "int", nullable: false),
                    PaidDate = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loadDetails", x => x.LoanDetailId);
                    table.ForeignKey(
                        name: "FK_loadDetails_loan_LoadId",
                        column: x => x.LoadId,
                        principalTable: "loan",
                        principalColumn: "LoadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_collateral_CollateralTypeId",
                table: "collateral",
                column: "CollateralTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_loadDetails_LoadId",
                table: "loadDetails",
                column: "LoadId");

            migrationBuilder.CreateIndex(
                name: "IX_loan_CollateralId",
                table: "loan",
                column: "CollateralId");

            migrationBuilder.CreateIndex(
                name: "IX_loan_CreditOfficerId",
                table: "loan",
                column: "CreditOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_loan_CustomerId",
                table: "loan",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "appUserPermissions");

            migrationBuilder.DropTable(
                name: "loadDetails");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "loan");

            migrationBuilder.DropTable(
                name: "collateral");

            migrationBuilder.DropTable(
                name: "creditOfficer");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "collateralType");
        }
    }
}
