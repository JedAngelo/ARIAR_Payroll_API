using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Library.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Backup",
                columns: table => new
                {
                    backup_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    backup_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    file_location = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Backup__AE70C8808B320FD9", x => x.backup_id);
                });

            migrationBuilder.CreateTable(
                name: "Personal_Information",
                columns: table => new
                {
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    age = table.Column<byte>(type: "tinyint", nullable: false),
                    created_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    modified_date = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    modified_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_date = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    deleted_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal_Information", x => x.personal_id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    position_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    position_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    modified_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    deleted_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__99A0E7A43CEB0EF8", x => x.position_id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    attendance_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    morning_in = table.Column<DateTime>(type: "datetime", nullable: false),
                    morning_out = table.Column<DateTime>(type: "datetime", nullable: false),
                    afternoon_in = table.Column<DateTime>(type: "datetime", nullable: false),
                    afternoon_out = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__20D6A9682236CCF9", x => x.attendance_id);
                    table.ForeignKey(
                        name: "FK_Attendance_Personal_Information",
                        column: x => x.personal_id,
                        principalTable: "Personal_Information",
                        principalColumn: "personal_id");
                });

            migrationBuilder.CreateTable(
                name: "Contact_Information",
                columns: table => new
                {
                    contact_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contact___024E7A86D7587303", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_Contact_Information_Personal_Information",
                        column: x => x.personal_id,
                        principalTable: "Personal_Information",
                        principalColumn: "personal_id");
                });

            migrationBuilder.CreateTable(
                name: "Employee_Biometric",
                columns: table => new
                {
                    record_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    biometric_data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    record_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__BFCFB4DD7DDB27F5", x => x.record_id);
                    table.ForeignKey(
                        name: "FK_Employee_Biometric_Personal_Information",
                        column: x => x.personal_id,
                        principalTable: "Personal_Information",
                        principalColumn: "personal_id");
                });

            migrationBuilder.CreateTable(
                name: "Leave",
                columns: table => new
                {
                    leave_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Leaves__743350BC431B4E71", x => x.leave_id);
                    table.ForeignKey(
                        name: "FK_Leaves_Personal_Information",
                        column: x => x.personal_id,
                        principalTable: "Personal_Information",
                        principalColumn: "personal_id");
                });

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    payroll_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_work_day = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    payment_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.payroll_id);
                    table.ForeignKey(
                        name: "FK_Payroll_Personal_Information",
                        column: x => x.personal_id,
                        principalTable: "Personal_Information",
                        principalColumn: "personal_id");
                });

            migrationBuilder.CreateTable(
                name: "Employment_Details",
                columns: table => new
                {
                    employment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    position_id = table.Column<int>(type: "int", nullable: false),
                    hire_date = table.Column<DateOnly>(type: "date", nullable: false),
                    pay_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    philhealth_employee_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    sss_employee_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    pagibig_employee_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    income_tax_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employme__63C1606468BACA47", x => x.employment_id);
                    table.ForeignKey(
                        name: "FK_Employment_Details_Personal_Information",
                        column: x => x.personal_id,
                        principalTable: "Personal_Information",
                        principalColumn: "personal_id");
                    table.ForeignKey(
                        name: "FK_Employment_Details_Position",
                        column: x => x.position_id,
                        principalTable: "Position",
                        principalColumn: "position_id");
                });

            migrationBuilder.CreateTable(
                name: "Gross_Salaries",
                columns: table => new
                {
                    salary_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payroll_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gross_salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Gross_Sa__A3C71C513EA2B2C1", x => x.salary_id);
                    table.ForeignKey(
                        name: "FK_Gross_Salaries_Payroll",
                        column: x => x.payroll_id,
                        principalTable: "Payroll",
                        principalColumn: "payroll_id");
                });

            migrationBuilder.CreateTable(
                name: "Net_Salaries",
                columns: table => new
                {
                    net_salary_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payroll_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    net_salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Net_Sala__6C719F7AC24E5CEE", x => x.net_salary_id);
                    table.ForeignKey(
                        name: "FK_Net_Salaries_Payroll",
                        column: x => x.payroll_id,
                        principalTable: "Payroll",
                        principalColumn: "payroll_id");
                });

            migrationBuilder.CreateTable(
                name: "Total_Deductions",
                columns: table => new
                {
                    total_deduction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payroll_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    philhealth_deduction_amount = table.Column<int>(type: "int", nullable: false),
                    pagibig_deduction_amount = table.Column<int>(type: "int", nullable: false),
                    sss_deduction_amount = table.Column<int>(type: "int", nullable: false),
                    income_tax_deduction_amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Total_De__9AF15027D43CBC2C", x => x.total_deduction_id);
                    table.ForeignKey(
                        name: "FK_Total_Deductions_Payroll",
                        column: x => x.payroll_id,
                        principalTable: "Payroll",
                        principalColumn: "payroll_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_personal_id",
                table: "Attendance",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Information_personal_id",
                table: "Contact_Information",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Biometric_personal_id",
                table: "Employee_Biometric",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employment_Details_personal_id",
                table: "Employment_Details",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employment_Details_position_id",
                table: "Employment_Details",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_Gross_Salaries_payroll_id",
                table: "Gross_Salaries",
                column: "payroll_id");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_personal_id",
                table: "Leave",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Net_Salaries_payroll_id",
                table: "Net_Salaries",
                column: "payroll_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_personal_id",
                table: "Payroll",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Total_Deductions_payroll_id",
                table: "Total_Deductions",
                column: "payroll_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Backup");

            migrationBuilder.DropTable(
                name: "Contact_Information");

            migrationBuilder.DropTable(
                name: "Employee_Biometric");

            migrationBuilder.DropTable(
                name: "Employment_Details");

            migrationBuilder.DropTable(
                name: "Gross_Salaries");

            migrationBuilder.DropTable(
                name: "Leave");

            migrationBuilder.DropTable(
                name: "Net_Salaries");

            migrationBuilder.DropTable(
                name: "Total_Deductions");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "Personal_Information");
        }
    }
}
