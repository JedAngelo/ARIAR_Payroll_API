using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Library.Migrations
{
    /// <inheritdoc />
    public partial class fixedrelationshiptomatchedERD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employment_Details_personal_id",
                table: "Employment_Details");

            migrationBuilder.DropIndex(
                name: "IX_Contact_Information_personal_id",
                table: "Contact_Information");

            migrationBuilder.CreateIndex(
                name: "IX_Employment_Details_personal_id",
                table: "Employment_Details",
                column: "personal_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Information_personal_id",
                table: "Contact_Information",
                column: "personal_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employment_Details_personal_id",
                table: "Employment_Details");

            migrationBuilder.DropIndex(
                name: "IX_Contact_Information_personal_id",
                table: "Contact_Information");

            migrationBuilder.CreateIndex(
                name: "IX_Employment_Details_personal_id",
                table: "Employment_Details",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Information_personal_id",
                table: "Contact_Information",
                column: "personal_id");
        }
    }
}
