using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Payroll_Library.Models;

public partial class AriarPayrollDbContext : DbContext
{
    public AriarPayrollDbContext()
    {
    }

    public AriarPayrollDbContext(DbContextOptions<AriarPayrollDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Backup> Backups { get; set; }

    public virtual DbSet<ContactInformation> ContactInformations { get; set; }

    public virtual DbSet<EmployeeBiometric> EmployeeBiometrics { get; set; }

    public virtual DbSet<EmploymentDetail> EmploymentDetails { get; set; }

    public virtual DbSet<GrossSalary> GrossSalaries { get; set; }

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<NetSalary> NetSalaries { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<PersonalInformation> PersonalInformations { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<TotalDeduction> TotalDeductions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("DefaultCon");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__20D6A9682236CCF9");

            entity.ToTable("Attendance");

            entity.HasIndex(e => e.PersonalId, "IX_Attendance_personal_id");

            entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");
            entity.Property(e => e.AfternoonIn)
                .HasPrecision(0)
                .HasColumnName("afternoon_in");
            entity.Property(e => e.AfternoonOut)
                .HasPrecision(0)
                .HasColumnName("afternoon_out");
            entity.Property(e => e.AttendanceDate).HasColumnName("attendance_date");
            entity.Property(e => e.MorningIn)
                .HasPrecision(0)
                .HasColumnName("morning_in");
            entity.Property(e => e.MorningOut)
                .HasPrecision(0)
                .HasColumnName("morning_out");
            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Personal).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Personal_Information");
        });

        modelBuilder.Entity<Backup>(entity =>
        {
            entity.HasKey(e => e.BackupId).HasName("PK__Backup__AE70C8808B320FD9");

            entity.ToTable("Backup");

            entity.Property(e => e.BackupId).HasColumnName("backup_id");
            entity.Property(e => e.BackupDate)
                .HasColumnType("datetime")
                .HasColumnName("backup_date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.FileLocation)
                .IsUnicode(false)
                .HasColumnName("file_location");
        });

        modelBuilder.Entity<ContactInformation>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contact___024E7A86D7587303");

            entity.ToTable("Contact_Information");

            entity.HasIndex(e => e.PersonalId, "IX_Contact_Information_personal_id").IsUnique();

            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Personal).WithOne(p => p.ContactInformation)
                .HasForeignKey<ContactInformation>(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Information_Personal_Information");
        });

        modelBuilder.Entity<EmployeeBiometric>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__Employee__BFCFB4DD7DDB27F5");

            entity.ToTable("Employee_Biometric");

            entity.HasIndex(e => e.PersonalId, "IX_Employee_Biometric_personal_id");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.BiometricData).HasColumnName("biometric_data");
            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.RecordDate).HasColumnName("record_date");

            entity.HasOne(d => d.Personal).WithMany(p => p.EmployeeBiometrics)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Biometric_Personal_Information");
        });

        modelBuilder.Entity<EmploymentDetail>(entity =>
        {
            entity.HasKey(e => e.EmploymentId).HasName("PK__Employme__63C1606468BACA47");

            entity.ToTable("Employment_Details");

            entity.HasIndex(e => e.PersonalId, "IX_Employment_Details_personal_id").IsUnique();

            entity.HasIndex(e => e.PositionId, "IX_Employment_Details_position_id");

            entity.Property(e => e.EmploymentId).HasColumnName("employment_id");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.IncomeTaxRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("income_tax_rate");
            entity.Property(e => e.PagibigEmployeeRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("pagibig_employee_rate");
            entity.Property(e => e.PayRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pay_rate");
            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.PhilhealthEmployeeRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("philhealth_employee_rate");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.SssEmployeeRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("sss_employee_rate");

            entity.HasOne(d => d.Personal).WithOne(p => p.EmploymentDetail)
                .HasForeignKey<EmploymentDetail>(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employment_Details_Personal_Information");

            entity.HasOne(d => d.Position).WithMany(p => p.EmploymentDetails)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employment_Details_Position");
        });

        modelBuilder.Entity<GrossSalary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Gross_Sa__A3C71C513EA2B2C1");

            entity.ToTable("Gross_Salaries");

            entity.HasIndex(e => e.PayrollId, "IX_Gross_Salaries_payroll_id").IsUnique();

            entity.Property(e => e.SalaryId).HasColumnName("salary_id");
            entity.Property(e => e.GrossSalaryAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gross_salary_amount");
            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");

            entity.HasOne(d => d.Payroll).WithOne(p => p.GrossSalary)
                .HasForeignKey<GrossSalary>(d => d.PayrollId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gross_Salaries_Payroll");
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__Leaves__743350BC431B4E71");

            entity.ToTable("Leave");

            entity.HasIndex(e => e.PersonalId, "IX_Leave_personal_id");

            entity.Property(e => e.LeaveId).HasColumnName("leave_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Personal).WithMany(p => p.Leaves)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leaves_Personal_Information");
        });

        modelBuilder.Entity<NetSalary>(entity =>
        {
            entity.HasKey(e => e.NetSalaryId).HasName("PK__Net_Sala__6C719F7AC24E5CEE");

            entity.ToTable("Net_Salaries");

            entity.HasIndex(e => e.PayrollId, "IX_Net_Salaries_payroll_id").IsUnique();

            entity.Property(e => e.NetSalaryId).HasColumnName("net_salary_id");
            entity.Property(e => e.NetSalaryAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("net_salary_amount");
            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");

            entity.HasOne(d => d.Payroll).WithOne(p => p.NetSalary)
                .HasForeignKey<NetSalary>(d => d.PayrollId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Net_Salaries_Payroll");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.ToTable("Payroll");

            entity.HasIndex(e => e.PersonalId, "IX_Payroll_personal_id");

            entity.Property(e => e.PayrollId)
                .ValueGeneratedNever()
                .HasColumnName("payroll_id");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.TotalWorkDay)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("total_work_day");

            entity.HasOne(d => d.Personal).WithMany(p => p.Payrolls)
                .HasForeignKey(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payroll_Personal_Information");
        });

        modelBuilder.Entity<PersonalInformation>(entity =>
        {
            entity.HasKey(e => e.PersonalId);

            entity.ToTable("Personal_Information");

            entity.Property(e => e.PersonalId)
                .ValueGeneratedNever()
                .HasColumnName("personal_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("deleted_by");
            entity.Property(e => e.DeletedDate).HasColumnName("deleted_date");
            entity.Property(e => e.EmployeeImage).HasColumnName("employee_image");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marital_status");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("middle_name");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__99A0E7A43CEB0EF8");

            entity.ToTable("Position");

            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("deleted_by");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("deleted_date");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedDate).HasColumnName("modified_date");
            entity.Property(e => e.PositionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<TotalDeduction>(entity =>
        {
            entity.HasKey(e => e.TotalDeductionId).HasName("PK__Total_De__9AF15027D43CBC2C");

            entity.ToTable("Total_Deductions");

            entity.HasIndex(e => e.PayrollId, "IX_Total_Deductions_payroll_id").IsUnique();

            entity.Property(e => e.TotalDeductionId).HasColumnName("total_deduction_id");
            entity.Property(e => e.IncomeTaxDeductionAmount).HasColumnName("income_tax_deduction_amount");
            entity.Property(e => e.PagibigDeductionAmount).HasColumnName("pagibig_deduction_amount");
            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");
            entity.Property(e => e.PhilhealthDeductionAmount).HasColumnName("philhealth_deduction_amount");
            entity.Property(e => e.SssDeductionAmount).HasColumnName("sss_deduction_amount");

            entity.HasOne(d => d.Payroll).WithOne(p => p.TotalDeduction)
                .HasForeignKey<TotalDeduction>(d => d.PayrollId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Total_Deductions_Payroll");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
