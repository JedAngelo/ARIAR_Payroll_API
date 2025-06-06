﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payroll_Library.Models;

#nullable disable

namespace Payroll_Library.Migrations
{
    [DbContext(typeof(AriarPayrollDbContext))]
    partial class AriarPayrollDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Payroll_Library.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("attendance_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"));

                    b.Property<TimeOnly?>("AfternoonIn")
                        .HasPrecision(0)
                        .HasColumnType("time(0)")
                        .HasColumnName("afternoon_in");

                    b.Property<TimeOnly?>("AfternoonOut")
                        .HasPrecision(0)
                        .HasColumnType("time(0)")
                        .HasColumnName("afternoon_out");

                    b.Property<DateOnly?>("AttendanceDate")
                        .HasColumnType("date")
                        .HasColumnName("attendance_date");

                    b.Property<string>("DayType")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("day_type");

                    b.Property<TimeOnly?>("MorningIn")
                        .HasPrecision(0)
                        .HasColumnType("time(0)")
                        .HasColumnName("morning_in");

                    b.Property<TimeOnly?>("MorningOut")
                        .HasPrecision(0)
                        .HasColumnType("time(0)")
                        .HasColumnName("morning_out");

                    b.Property<decimal>("PayMultiplier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(5,2)")
                        .HasDefaultValue(1m)
                        .HasColumnName("pay_multiplier");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personal_id");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.HasKey("AttendanceId")
                        .HasName("PK__Attendan__20D6A9682236CCF9");

                    b.HasIndex(new[] { "PersonalId" }, "IX_Attendance_personal_id");

                    b.ToTable("Attendance", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.Backup", b =>
                {
                    b.Property<int>("BackupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("backup_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BackupId"));

                    b.Property<DateTime>("BackupDate")
                        .HasColumnType("datetime")
                        .HasColumnName("backup_date");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("FileLocation")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("file_location");

                    b.HasKey("BackupId")
                        .HasName("PK__Backup__AE70C8808B320FD9");

                    b.ToTable("Backup", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.ContactInformation", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("contact_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personal_id");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("phone_number");

                    b.HasKey("ContactId")
                        .HasName("PK__Contact___024E7A86D7587303");

                    b.HasIndex(new[] { "PersonalId" }, "IX_Contact_Information_personal_id")
                        .IsUnique();

                    b.ToTable("Contact_Information", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.EmployeeBiometric", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("record_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecordId"));

                    b.Property<byte[]>("BiometricData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("biometric_data");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personal_id");

                    b.Property<DateTime>("RecordDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("record_date");

                    b.HasKey("RecordId")
                        .HasName("PK__Employee__BFCFB4DD7DDB27F5");

                    b.HasIndex(new[] { "PersonalId" }, "IX_Employee_Biometric_personal_id");

                    b.ToTable("Employee_Biometric", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.EmploymentDetail", b =>
                {
                    b.Property<int>("EmploymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("employment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmploymentId"));

                    b.Property<DateOnly>("HireDate")
                        .HasColumnType("date")
                        .HasColumnName("hire_date");

                    b.Property<decimal>("IncomeTaxRate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10, 2)")
                        .HasDefaultValue(0m)
                        .HasColumnName("income_tax_rate");

                    b.Property<decimal>("PayRate")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("pay_rate");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personal_id");

                    b.Property<int>("PositionId")
                        .HasColumnType("int")
                        .HasColumnName("position_id");

                    b.HasKey("EmploymentId")
                        .HasName("PK__Employme__63C1606468BACA47");

                    b.HasIndex(new[] { "PersonalId" }, "IX_Employment_Details_personal_id")
                        .IsUnique();

                    b.HasIndex(new[] { "PositionId" }, "IX_Employment_Details_position_id");

                    b.ToTable("Employment_Details", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.Leave", b =>
                {
                    b.Property<int>("LeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("leave_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaveId"));

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personal_id");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.HasKey("LeaveId")
                        .HasName("PK__Leaves__743350BC431B4E71");

                    b.HasIndex(new[] { "PersonalId" }, "IX_Leave_personal_id");

                    b.ToTable("Leave", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.Payroll", b =>
                {
                    b.Property<Guid>("PayrollId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("payroll_id");

                    b.Property<decimal>("Commissions")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("commissions");

                    b.Property<decimal>("EmployeePagibigShare")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("employee_pagibig_share");

                    b.Property<decimal>("EmployeePhilhealthShare")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("employee_philhealth_share");

                    b.Property<decimal>("EmployeeSssShare")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("employee_sss_share");

                    b.Property<decimal>("EmployerPagibigShare")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("employer_pagibig_share");

                    b.Property<decimal>("EmployerPhilhealthShare")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("employer_philhealth_share");

                    b.Property<decimal>("EmployerSssShare")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("employer_sss_share");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<decimal>("GrossSalary")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("gross_salary");

                    b.Property<decimal>("NetSalary")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("net_salary");

                    b.Property<decimal>("OtherDeductions")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("other_deductions");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personal_id");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.Property<decimal>("TotalWorkDay")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("total_work_day");

                    b.HasKey("PayrollId");

                    b.HasIndex(new[] { "PersonalId" }, "IX_Payroll_personal_id");

                    b.ToTable("Payroll", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.PersonalInformation", b =>
                {
                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("personal_id");

                    b.Property<byte>("Age")
                        .HasColumnType("tinyint")
                        .HasColumnName("age");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_date");

                    b.Property<byte[]>("EmployeeImage")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("employee_image");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("gender");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("last_name");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("marital_status");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("middle_name");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Suffix")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("suffix");

                    b.HasKey("PersonalId");

                    b.ToTable("Personal_Information", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("position_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("deleted_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("position_name");

                    b.HasKey("PositionId")
                        .HasName("PK__Position__99A0E7A43CEB0EF8");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.SssMonthlyCredit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("EmployeeShare")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("employee_share");

                    b.Property<decimal>("EmployerShare")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("employer_share");

                    b.Property<decimal>("LowerLimit")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("lower_limit");

                    b.Property<decimal>("MonthlySalaryCredit")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("monthly_salary_credit");

                    b.Property<decimal>("UpperLimit")
                        .HasColumnType("decimal(10,5)")
                        .HasColumnName("upper_limit");

                    b.HasKey("Id");

                    b.ToTable("SSS_Monthly_Credit", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.SystemSettings", b =>
                {
                    b.Property<int>("SettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("settings_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SettingsId"));

                    b.Property<string>("AttendanceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("attendance_type");

                    b.Property<TimeOnly>("EarlyOutEndsAfternoon")
                        .HasColumnType("time")
                        .HasColumnName("early_out_ends_afternoon");

                    b.Property<TimeOnly>("EarlyOutEndsMorning")
                        .HasColumnType("time")
                        .HasColumnName("early_out_ends_morning");

                    b.Property<decimal>("EmployerPagibigRate")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("employer_pagibig_rate");

                    b.Property<decimal>("EmployerPhilhealthRate")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("employer_philhealth_rate");

                    b.Property<decimal>("EmployerSssRate")
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("employer_sss_rate");

                    b.Property<TimeOnly>("LateStartTimeAfternoon")
                        .HasColumnType("time")
                        .HasColumnName("late_start_time_afternoon");

                    b.Property<TimeOnly>("LateStartTimeMorning")
                        .HasColumnType("time")
                        .HasColumnName("late_start_time_morning");

                    b.Property<bool>("PasswordlessManualAttendance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("passwordless_manual_attendance");

                    b.HasKey("SettingsId");

                    b.ToTable("Settings", (string)null);
                });

            modelBuilder.Entity("Payroll_Library.Models.Attendance", b =>
                {
                    b.HasOne("Payroll_Library.Models.PersonalInformation", "Personal")
                        .WithMany("Attendances")
                        .HasForeignKey("PersonalId")
                        .IsRequired()
                        .HasConstraintName("FK_Attendance_Personal_Information");

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("Payroll_Library.Models.ContactInformation", b =>
                {
                    b.HasOne("Payroll_Library.Models.PersonalInformation", "Personal")
                        .WithOne("ContactInformation")
                        .HasForeignKey("Payroll_Library.Models.ContactInformation", "PersonalId")
                        .IsRequired()
                        .HasConstraintName("FK_Contact_Information_Personal_Information");

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("Payroll_Library.Models.EmployeeBiometric", b =>
                {
                    b.HasOne("Payroll_Library.Models.PersonalInformation", "Personal")
                        .WithMany("EmployeeBiometrics")
                        .HasForeignKey("PersonalId")
                        .IsRequired()
                        .HasConstraintName("FK_Employee_Biometric_Personal_Information");

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("Payroll_Library.Models.EmploymentDetail", b =>
                {
                    b.HasOne("Payroll_Library.Models.PersonalInformation", "Personal")
                        .WithOne("EmploymentDetail")
                        .HasForeignKey("Payroll_Library.Models.EmploymentDetail", "PersonalId")
                        .IsRequired()
                        .HasConstraintName("FK_Employment_Details_Personal_Information");

                    b.HasOne("Payroll_Library.Models.Position", "Position")
                        .WithMany("EmploymentDetails")
                        .HasForeignKey("PositionId")
                        .IsRequired()
                        .HasConstraintName("FK_Employment_Details_Position");

                    b.Navigation("Personal");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Payroll_Library.Models.Leave", b =>
                {
                    b.HasOne("Payroll_Library.Models.PersonalInformation", "Personal")
                        .WithMany("Leaves")
                        .HasForeignKey("PersonalId")
                        .IsRequired()
                        .HasConstraintName("FK_Leaves_Personal_Information");

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("Payroll_Library.Models.Payroll", b =>
                {
                    b.HasOne("Payroll_Library.Models.PersonalInformation", "Personal")
                        .WithMany("Payrolls")
                        .HasForeignKey("PersonalId")
                        .IsRequired()
                        .HasConstraintName("FK_Payroll_Personal_Information");

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("Payroll_Library.Models.PersonalInformation", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("ContactInformation");

                    b.Navigation("EmployeeBiometrics");

                    b.Navigation("EmploymentDetail");

                    b.Navigation("Leaves");

                    b.Navigation("Payrolls");
                });

            modelBuilder.Entity("Payroll_Library.Models.Position", b =>
                {
                    b.Navigation("EmploymentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
