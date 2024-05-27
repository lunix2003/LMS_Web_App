using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class EditTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_customer_CustomerId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_appUserPermissions_AppUser_AppUserId",
                table: "appUserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_collateral_collateralType_CollateralTypeId",
                table: "collateral");

            migrationBuilder.DropForeignKey(
                name: "FK_loadDetails_loan_LoadId",
                table: "loadDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_loan_collateral_CollateralId",
                table: "loan");

            migrationBuilder.DropForeignKey(
                name: "FK_loan_creditOfficer_CreditOfficerId",
                table: "loan");

            migrationBuilder.DropForeignKey(
                name: "FK_loan_customer_CustomerId",
                table: "loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loan",
                table: "loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loadDetails",
                table: "loadDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer",
                table: "customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_creditOfficer",
                table: "creditOfficer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_collateralType",
                table: "collateralType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_collateral",
                table: "collateral");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appUserPermissions",
                table: "appUserPermissions");

            migrationBuilder.RenameTable(
                name: "loan",
                newName: "Loan");

            migrationBuilder.RenameTable(
                name: "loadDetails",
                newName: "LoadDetails");

            migrationBuilder.RenameTable(
                name: "customer",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "creditOfficer",
                newName: "CreditOfficer");

            migrationBuilder.RenameTable(
                name: "collateralType",
                newName: "CollateralType");

            migrationBuilder.RenameTable(
                name: "collateral",
                newName: "Collateral");

            migrationBuilder.RenameTable(
                name: "appUserPermissions",
                newName: "AppUserPermissions");

            migrationBuilder.RenameIndex(
                name: "IX_loan_CustomerId",
                table: "Loan",
                newName: "IX_Loan_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_loan_CreditOfficerId",
                table: "Loan",
                newName: "IX_Loan_CreditOfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_loan_CollateralId",
                table: "Loan",
                newName: "IX_Loan_CollateralId");

            migrationBuilder.RenameIndex(
                name: "IX_loadDetails_LoadId",
                table: "LoadDetails",
                newName: "IX_LoadDetails_LoadId");

            migrationBuilder.RenameIndex(
                name: "IX_collateral_CollateralTypeId",
                table: "Collateral",
                newName: "IX_Collateral_CollateralTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loan",
                table: "Loan",
                column: "LoadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoadDetails",
                table: "LoadDetails",
                column: "LoanDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditOfficer",
                table: "CreditOfficer",
                column: "CreditOfficerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollateralType",
                table: "CollateralType",
                column: "CollateralTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collateral",
                table: "Collateral",
                column: "CollateralId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserPermissions",
                table: "AppUserPermissions",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_CustomerId",
                table: "Address",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserPermissions_AppUser_AppUserId",
                table: "AppUserPermissions",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collateral_CollateralType_CollateralTypeId",
                table: "Collateral",
                column: "CollateralTypeId",
                principalTable: "CollateralType",
                principalColumn: "CollateralTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadDetails_Loan_LoadId",
                table: "LoadDetails",
                column: "LoadId",
                principalTable: "Loan",
                principalColumn: "LoadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Collateral_CollateralId",
                table: "Loan",
                column: "CollateralId",
                principalTable: "Collateral",
                principalColumn: "CollateralId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_CreditOfficer_CreditOfficerId",
                table: "Loan",
                column: "CreditOfficerId",
                principalTable: "CreditOfficer",
                principalColumn: "CreditOfficerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Customer_CustomerId",
                table: "Loan",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_CustomerId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserPermissions_AppUser_AppUserId",
                table: "AppUserPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Collateral_CollateralType_CollateralTypeId",
                table: "Collateral");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadDetails_Loan_LoadId",
                table: "LoadDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Collateral_CollateralId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_CreditOfficer_CreditOfficerId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Customer_CustomerId",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loan",
                table: "Loan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoadDetails",
                table: "LoadDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditOfficer",
                table: "CreditOfficer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollateralType",
                table: "CollateralType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collateral",
                table: "Collateral");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserPermissions",
                table: "AppUserPermissions");

            migrationBuilder.RenameTable(
                name: "Loan",
                newName: "loan");

            migrationBuilder.RenameTable(
                name: "LoadDetails",
                newName: "loadDetails");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "customer");

            migrationBuilder.RenameTable(
                name: "CreditOfficer",
                newName: "creditOfficer");

            migrationBuilder.RenameTable(
                name: "CollateralType",
                newName: "collateralType");

            migrationBuilder.RenameTable(
                name: "Collateral",
                newName: "collateral");

            migrationBuilder.RenameTable(
                name: "AppUserPermissions",
                newName: "appUserPermissions");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_CustomerId",
                table: "loan",
                newName: "IX_loan_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_CreditOfficerId",
                table: "loan",
                newName: "IX_loan_CreditOfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_CollateralId",
                table: "loan",
                newName: "IX_loan_CollateralId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadDetails_LoadId",
                table: "loadDetails",
                newName: "IX_loadDetails_LoadId");

            migrationBuilder.RenameIndex(
                name: "IX_Collateral_CollateralTypeId",
                table: "collateral",
                newName: "IX_collateral_CollateralTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loan",
                table: "loan",
                column: "LoadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loadDetails",
                table: "loadDetails",
                column: "LoanDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer",
                table: "customer",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_creditOfficer",
                table: "creditOfficer",
                column: "CreditOfficerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_collateralType",
                table: "collateralType",
                column: "CollateralTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_collateral",
                table: "collateral",
                column: "CollateralId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appUserPermissions",
                table: "appUserPermissions",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_customer_CustomerId",
                table: "Address",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appUserPermissions_AppUser_AppUserId",
                table: "appUserPermissions",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_collateral_collateralType_CollateralTypeId",
                table: "collateral",
                column: "CollateralTypeId",
                principalTable: "collateralType",
                principalColumn: "CollateralTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loadDetails_loan_LoadId",
                table: "loadDetails",
                column: "LoadId",
                principalTable: "loan",
                principalColumn: "LoadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loan_collateral_CollateralId",
                table: "loan",
                column: "CollateralId",
                principalTable: "collateral",
                principalColumn: "CollateralId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loan_creditOfficer_CreditOfficerId",
                table: "loan",
                column: "CreditOfficerId",
                principalTable: "creditOfficer",
                principalColumn: "CreditOfficerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loan_customer_CustomerId",
                table: "loan",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
