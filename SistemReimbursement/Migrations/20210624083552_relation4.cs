using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemReimbursement.Migrations
{
    public partial class relation4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Account_RoleId",
                table: "TB_M_Account",
                column: "RoleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Account_TB_M_Role_RoleId",
                table: "TB_M_Account",
                column: "RoleId",
                principalTable: "TB_M_Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Account_TB_M_Role_RoleId",
                table: "TB_M_Account");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Account_RoleId",
                table: "TB_M_Account");
        }
    }
}
