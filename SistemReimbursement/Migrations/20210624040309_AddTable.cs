using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemReimbursement.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Reimbursement",
                columns: table => new
                {
                    ReimbursementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestAmount = table.Column<int>(type: "int", nullable: false),
                    PaidAmount = table.Column<int>(type: "int", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileAttachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ReimbursementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_Attachment", x => x.AttachmentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Category");

            migrationBuilder.DropTable(
                name: "TB_M_Reimbursement");

            migrationBuilder.DropTable(
                name: "TB_TR_Attachment");
        }
    }
}
