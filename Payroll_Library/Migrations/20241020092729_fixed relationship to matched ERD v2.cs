using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Library.Migrations
{
    /// <inheritdoc />
    public partial class fixedrelationshiptomatchedERDv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Total_Deductions_payroll_id",
                table: "Total_Deductions");

            migrationBuilder.DropIndex(
                name: "IX_Net_Salaries_payroll_id",
                table: "Net_Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Gross_Salaries_payroll_id",
                table: "Gross_Salaries");

            migrationBuilder.CreateIndex(
                name: "IX_Total_Deductions_payroll_id",
                table: "Total_Deductions",
                column: "payroll_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Net_Salaries_payroll_id",
                table: "Net_Salaries",
                column: "payroll_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gross_Salaries_payroll_id",
                table: "Gross_Salaries",
                column: "payroll_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Total_Deductions_payroll_id",
                table: "Total_Deductions");

            migrationBuilder.DropIndex(
                name: "IX_Net_Salaries_payroll_id",
                table: "Net_Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Gross_Salaries_payroll_id",
                table: "Gross_Salaries");

            migrationBuilder.CreateIndex(
                name: "IX_Total_Deductions_payroll_id",
                table: "Total_Deductions",
                column: "payroll_id");

            migrationBuilder.CreateIndex(
                name: "IX_Net_Salaries_payroll_id",
                table: "Net_Salaries",
                column: "payroll_id");

            migrationBuilder.CreateIndex(
                name: "IX_Gross_Salaries_payroll_id",
                table: "Gross_Salaries",
                column: "payroll_id");
        }
    }
}
