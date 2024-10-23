using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Library.Migrations
{
    /// <inheritdoc />
    public partial class fixedtimeprecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "morning_out",
                table: "Attendance",
                type: "time(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "morning_in",
                table: "Attendance",
                type: "time(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "afternoon_out",
                table: "Attendance",
                type: "time(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "afternoon_in",
                table: "Attendance",
                type: "time(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "morning_out",
                table: "Attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "morning_in",
                table: "Attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "afternoon_out",
                table: "Attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "afternoon_in",
                table: "Attendance",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time(0)",
                oldPrecision: 0,
                oldNullable: true);
        }
    }
}
