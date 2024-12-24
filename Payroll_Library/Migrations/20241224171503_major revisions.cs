using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Library.Migrations
{
    /// <inheritdoc />
    public partial class majorrevisions : Migration
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
                    suffix = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    marital_status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    age = table.Column<byte>(type: "tinyint", nullable: false),
                    employee_image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    deleted_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    deleted_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__99A0E7A43CEB0EF8", x => x.position_id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    settings_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employer_sss_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employer_philhealth_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employer_pagibig_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    attendance_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordless_manual_attendance = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    late_start_time_morning = table.Column<TimeOnly>(type: "time", nullable: false),
                    late_start_time_afternoon = table.Column<TimeOnly>(type: "time", nullable: false),
                    early_out_ends_morning = table.Column<TimeOnly>(type: "time", nullable: false),
                    early_out_ends_afternoon = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.settings_id);
                });

            migrationBuilder.CreateTable(
                name: "SSS_Monthly_Credit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lower_limit = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    upper_limit = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    monthly_salary_credit = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    employee_share = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    employer_share = table.Column<decimal>(type: "decimal(10,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSS_Monthly_Credit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    attendance_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    morning_in = table.Column<TimeOnly>(type: "time(0)", precision: 0, nullable: true),
                    morning_out = table.Column<TimeOnly>(type: "time(0)", precision: 0, nullable: true),
                    afternoon_in = table.Column<TimeOnly>(type: "time(0)", precision: 0, nullable: true),
                    afternoon_out = table.Column<TimeOnly>(type: "time(0)", precision: 0, nullable: true),
                    attendance_date = table.Column<DateOnly>(type: "date", nullable: true),
                    pay_multiplier = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 1m),
                    day_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    record_date = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    gross_salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    net_salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employer_sss_share = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employer_pagibig_share = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employer_philhealth_share = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employee_sss_share = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employee_pagibig_share = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    employee_philhealth_share = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    other_deductions = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    commissions = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
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
                    income_tax_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m)
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

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_personal_id",
                table: "Attendance",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Information_personal_id",
                table: "Contact_Information",
                column: "personal_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Biometric_personal_id",
                table: "Employee_Biometric",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employment_Details_personal_id",
                table: "Employment_Details",
                column: "personal_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employment_Details_position_id",
                table: "Employment_Details",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_personal_id",
                table: "Leave",
                column: "personal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_personal_id",
                table: "Payroll",
                column: "personal_id");
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
                name: "Leave");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SSS_Monthly_Credit");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Personal_Information");
        }
    }
}
