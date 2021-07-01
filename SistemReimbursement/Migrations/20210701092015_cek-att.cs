using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemReimbursement.Migrations
{
    public partial class cekatt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_User",
                columns: table => new
                {
                    Nik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerNik = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_User", x => x.Nik);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Account",
                columns: table => new
                {
                    Nik = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Account", x => x.Nik);
                    table.ForeignKey(
                        name: "FK_TB_M_Account_TB_M_User_Nik",
                        column: x => x.Nik,
                        principalTable: "TB_M_User",
                        principalColumn: "Nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Reimbursement",
                columns: table => new
                {
                    ReimbursementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    FinanceApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinanceApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    FinanceApprovalNik = table.Column<int>(type: "int", nullable: false),
                    Nik = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Reimbursement", x => x.ReimbursementId);
                    table.ForeignKey(
                        name: "FK_TB_M_Reimbursement_TB_M_Account_Nik",
                        column: x => x.Nik,
                        principalTable: "TB_M_Account",
                        principalColumn: "Nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_AccountRole",
                columns: table => new
                {
                    AccountRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nik = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_AccountRole", x => x.AccountRoleId);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRole_TB_M_Account_Nik",
                        column: x => x.Nik,
                        principalTable: "TB_M_Account",
                        principalColumn: "Nik",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRole_TB_M_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RequestAmount = table.Column<int>(type: "int", nullable: false),
                    PaidAmount = table.Column<int>(type: "int", nullable: false),
                    ReimbursementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_Attachment", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_TB_TR_Attachment_TB_M_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TB_M_Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TR_Attachment_TB_M_Reimbursement_ReimbursementId",
                        column: x => x.ReimbursementId,
                        principalTable: "TB_M_Reimbursement",
                        principalColumn: "ReimbursementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Reimbursement_Nik",
                table: "TB_M_Reimbursement",
                column: "Nik");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRole_Nik",
                table: "TB_TR_AccountRole",
                column: "Nik");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRole_RoleId",
                table: "TB_TR_AccountRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_Attachment_CategoryId",
                table: "TB_TR_Attachment",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_Attachment_ReimbursementId",
                table: "TB_TR_Attachment",
                column: "ReimbursementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TR_AccountRole");

            migrationBuilder.DropTable(
                name: "TB_TR_Attachment");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_M_Category");

            migrationBuilder.DropTable(
                name: "TB_M_Reimbursement");

            migrationBuilder.DropTable(
                name: "TB_M_Account");

            migrationBuilder.DropTable(
                name: "TB_M_User");
        }
    }
}
