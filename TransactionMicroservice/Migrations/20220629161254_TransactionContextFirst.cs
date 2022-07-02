using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionMicroservice.Migrations
{
    public partial class TransactionContextFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactionHistories",
                columns: table => new
                {
                    Transaction_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account_ID = table.Column<int>(nullable: false),
                    transaction_status_code = table.Column<int>(nullable: true),
                    Date_of_Transaction = table.Column<DateTime>(nullable: true),
                    Amount_of_Transaction = table.Column<float>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionHistories", x => x.Transaction_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactionHistories");
        }
    }
}
