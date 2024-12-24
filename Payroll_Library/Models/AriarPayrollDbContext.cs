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

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<PersonalInformation> PersonalInformations { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<SystemSettings> SystemSettings { get; set; }

    public virtual DbSet<SssMonthlyCredit> SssMonthlyCredits { get; set; }

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

            entity.Property(e => e.PayMultiplier)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(1)
                .HasColumnName("pay_multiplier");

            entity.Property(e => e.DayType).HasColumnName("day_type");

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
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("income_tax_rate")
                .HasDefaultValue(0.0);

            entity.Property(e => e.PayRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pay_rate");

            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
                
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Personal).WithOne(p => p.EmploymentDetail)
                .HasForeignKey<EmploymentDetail>(d => d.PersonalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employment_Details_Personal_Information");

            entity.HasOne(d => d.Position).WithMany(p => p.EmploymentDetails)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employment_Details_Position");
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


        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.ToTable("Payroll");

            entity.HasIndex(e => e.PersonalId, "IX_Payroll_personal_id");

            entity.Property(e => e.PayrollId)
                .ValueGeneratedNever()
                .HasColumnName("payroll_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PersonalId).HasColumnName("personal_id");
            entity.Property(e => e.TotalWorkDay)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("total_work_day");

            entity.Property(e => e.GrossSalary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gross_salary");

            entity.Property(e => e.NetSalary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("net_salary");

            entity.Property(e => e.EmployeePhilhealthShare)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employee_philhealth_share");

            entity.Property(e => e.EmployerPhilhealthShare)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employer_philhealth_share");

            entity.Property(e => e.EmployeePagibigShare)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employee_pagibig_share");

            entity.Property(e => e.EmployerPagibigShare)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employer_pagibig_share");

            entity.Property(e => e.EmployeeSssShare)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employee_sss_share");

            entity.Property(e => e.EmployerSssShare)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("employer_sss_share");

            entity.Property(e => e.OtherDeductions)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("other_deductions");

            entity.Property(e => e.Commissions)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("commissions");


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
            entity.Property(e => e.Suffix)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("suffix");
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

        

        modelBuilder.Entity<SssMonthlyCredit>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("SSS_Monthly_Credit");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LowerLimit)
                .HasColumnType("decimal(10,5)")
                .HasColumnName("lower_limit");

            entity.Property(e => e.UpperLimit)
                .HasColumnType("decimal(10,5)")
                .HasColumnName("upper_limit");

            entity.Property(e => e.MonthlySalaryCredit)
                .HasColumnType("decimal(10,5)")
                .HasColumnName("monthly_salary_credit");

            entity.Property(e => e.EmployerShare)
                .HasColumnType("decimal(10,5)")
                .HasColumnName("employer_share");

            entity.Property(e => e.EmployeeShare)
                .HasColumnType("decimal(10,5)")
                .HasColumnName("employee_share");

        });

        modelBuilder.Entity<SystemSettings>(entity =>
        {
            entity.HasKey(e => e.SettingsId);

            entity.ToTable("Settings");

            entity.Property(e => e.SettingsId).HasColumnName("settings_id");
            entity.Property(e => e.EmployerSssRate)
                .HasColumnName("employer_sss_rate")
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.EmployerPhilhealthRate)
                .HasColumnName("employer_philhealth_rate")
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.EmployerPagibigRate)
                .HasColumnName("employer_pagibig_rate")
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.AttendanceType)
               .HasColumnName("attendance_type")
               .IsRequired();


            entity.Property(e => e.PasswordlessManualAttendance)
                .HasColumnName("passwordless_manual_attendance")
                .HasDefaultValue(false);

            entity.Property(e => e.LateStartTimeMorning)
                .HasColumnName("late_start_time_morning");

            entity.Property(e => e.LateStartTimeAfternoon)
                .HasColumnName("late_start_time_afternoon");

            entity.Property(e => e.EarlyOutEndsMorning)
               .HasColumnName("early_out_ends_morning");

            entity.Property(e => e.EarlyOutEndsAfternoon)
                .HasColumnName("early_out_ends_afternoon");


        });



        OnModelCreatingPartial(modelBuilder);


        // --TO DO FOR PAYROLL--
        bool settingExist = modelBuilder.Model.FindEntityType(typeof(SystemSettings))!.GetSeedData().Any();

        if (settingExist)
        {
            modelBuilder.Entity<SystemSettings>().HasData(
                new SystemSettings
                {
                    SettingsId = 1,

                    //Payroll
                    EmployerPagibigRate = 0.6m,
                    EmployerPhilhealthRate = 0.5m,
                    EmployerSssRate = 0.0m,

                    //General Settings
                    AttendanceType = "IN/OUT",
                    LateStartTimeMorning = new TimeOnly(8, 0, 0),
                    LateStartTimeAfternoon = new TimeOnly(1, 0, 0),
                    PasswordlessManualAttendance = false,
                    EarlyOutEndsAfternoon = new TimeOnly(16,30,0),
                    EarlyOutEndsMorning = new TimeOnly(11,30,0)
                    
                }
            );
        }




        //if (!modelBuilder.Model.GetEntityTypes().Any(e => e.ClrType == typeof(SssMonthlyCredit) && this.SssMonthlyCredits.Any()))
        //{
        //    modelBuilder.Entity<SssMonthlyCredit>().HasData(
        //        new SssMonthlyCredit { Id = 1, LowerLimit = 0, UpperLimit = 4249.99m, MonthlySalaryCredit = 4000, EmployeeShare = 180.00m, EmployerShare = 380.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 2, LowerLimit = 4250, UpperLimit = 4749.99m, MonthlySalaryCredit = 4500, EmployeeShare = 202.50m, EmployerShare = 427.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 3, LowerLimit = 4750, UpperLimit = 5249.99m, MonthlySalaryCredit = 5000, EmployeeShare = 225.00m, EmployerShare = 475.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 4, LowerLimit = 5250, UpperLimit = 5749.99m, MonthlySalaryCredit = 5500, EmployeeShare = 247.50m, EmployerShare = 522.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 5, LowerLimit = 5750, UpperLimit = 6249.99m, MonthlySalaryCredit = 6000, EmployeeShare = 270.00m, EmployerShare = 570.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 6, LowerLimit = 6250, UpperLimit = 6749.99m, MonthlySalaryCredit = 6500, EmployeeShare = 292.50m, EmployerShare = 617.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 7, LowerLimit = 6750, UpperLimit = 7249.99m, MonthlySalaryCredit = 7000, EmployeeShare = 315.00m, EmployerShare = 665.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 8, LowerLimit = 7250, UpperLimit = 7749.99m, MonthlySalaryCredit = 7500, EmployeeShare = 337.50m, EmployerShare = 712.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 9, LowerLimit = 7750, UpperLimit = 8249.99m, MonthlySalaryCredit = 8000, EmployeeShare = 360.00m, EmployerShare = 760.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 10, LowerLimit = 8250, UpperLimit = 8749.99m, MonthlySalaryCredit = 8500, EmployeeShare = 382.50m, EmployerShare = 807.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 11, LowerLimit = 8750, UpperLimit = 9249.99m, MonthlySalaryCredit = 9000, EmployeeShare = 405.00m, EmployerShare = 855.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 12, LowerLimit = 9250, UpperLimit = 9749.99m, MonthlySalaryCredit = 9500, EmployeeShare = 427.50m, EmployerShare = 902.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 13, LowerLimit = 9750, UpperLimit = 10249.99m, MonthlySalaryCredit = 10000, EmployeeShare = 450.00m, EmployerShare = 950.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 14, LowerLimit = 10250, UpperLimit = 10749.99m, MonthlySalaryCredit = 10500, EmployeeShare = 472.50m, EmployerShare = 997.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 15, LowerLimit = 10750, UpperLimit = 11249.99m, MonthlySalaryCredit = 11000, EmployeeShare = 495.00m, EmployerShare = 1045.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 16, LowerLimit = 11250, UpperLimit = 11749.99m, MonthlySalaryCredit = 11500, EmployeeShare = 517.50m, EmployerShare = 1092.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 17, LowerLimit = 11750, UpperLimit = 12249.99m, MonthlySalaryCredit = 12000, EmployeeShare = 540.00m, EmployerShare = 1140.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 18, LowerLimit = 12250, UpperLimit = 12749.99m, MonthlySalaryCredit = 12500, EmployeeShare = 562.50m, EmployerShare = 1187.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 19, LowerLimit = 12750, UpperLimit = 13249.99m, MonthlySalaryCredit = 13000, EmployeeShare = 585.00m, EmployerShare = 1235.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 20, LowerLimit = 13250, UpperLimit = 13749.99m, MonthlySalaryCredit = 13500, EmployeeShare = 607.50m, EmployerShare = 1282.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 21, LowerLimit = 13750, UpperLimit = 14249.99m, MonthlySalaryCredit = 14000, EmployeeShare = 630.00m, EmployerShare = 1330.00m + 10.00m },
        //        new SssMonthlyCredit { Id = 22, LowerLimit = 14250, UpperLimit = 14749.99m, MonthlySalaryCredit = 14500, EmployeeShare = 652.50m, EmployerShare = 1377.50m + 10.00m },
        //        new SssMonthlyCredit { Id = 23, LowerLimit = 14750, UpperLimit = 15249.99m, MonthlySalaryCredit = 15000, EmployeeShare = 675.00m, EmployerShare = 1425.00m + 30.00m },
        //        new SssMonthlyCredit { Id = 24, LowerLimit = 15250, UpperLimit = 15749.99m, MonthlySalaryCredit = 15500, EmployeeShare = 697.50m, EmployerShare = 1472.50m + 30.00m },
        //        new SssMonthlyCredit { Id = 25, LowerLimit = 15750, UpperLimit = 16249.99m, MonthlySalaryCredit = 16000, EmployeeShare = 720.00m, EmployerShare = 1520.00m + 30.00m },
        //        new SssMonthlyCredit { Id = 26, LowerLimit = 16250, UpperLimit = 16749.99m, MonthlySalaryCredit = 16500, EmployeeShare = 742.50m, EmployerShare = 1567.50m + 30.00m },
        //        new SssMonthlyCredit { Id = 27, LowerLimit = 16750, UpperLimit = 17249.99m, MonthlySalaryCredit = 17000, EmployeeShare = 765.00m, EmployerShare = 1615.00m + 30.00m },
        //        new SssMonthlyCredit { Id = 28, LowerLimit = 17250, UpperLimit = 17749.99m, MonthlySalaryCredit = 17500, EmployeeShare = 787.50m, EmployerShare = 1662.50m + 30.00m },
        //        new SssMonthlyCredit { Id = 29, LowerLimit = 17750, UpperLimit = 18249.99m, MonthlySalaryCredit = 18000, EmployeeShare = 810.00m, EmployerShare = 1710.00m + 30.00m },
        //        new SssMonthlyCredit { Id = 30, LowerLimit = 18250, UpperLimit = 18749.99m, MonthlySalaryCredit = 18500, EmployeeShare = 832.50m, EmployerShare = 1757.50m + 30.00m },
        //        new SssMonthlyCredit { Id = 31, LowerLimit = 18750, UpperLimit = 19249.99m, MonthlySalaryCredit = 19000, EmployeeShare = 855.00m, EmployerShare = 1805.00m + 30.00m },
        //        new SssMonthlyCredit { Id = 32, LowerLimit = 19250, UpperLimit = 19749.99m, MonthlySalaryCredit = 19500, EmployeeShare = 877.50m, EmployerShare = 1852.50m + 30.00m },
        //        new SssMonthlyCredit { Id = 33, LowerLimit = 19750, UpperLimit = 20249.99m, MonthlySalaryCredit = 20000, EmployeeShare = 900.00m, EmployerShare = 1930.00m + 30.00m },
        //        new SssMonthlyCredit { Id = 34, LowerLimit = 20250, UpperLimit = 20749.99m, MonthlySalaryCredit = 20500, EmployeeShare = 922.50m, EmployerShare = 1930.00m + 70.00m },
        //        new SssMonthlyCredit { Id = 35, LowerLimit = 20750, UpperLimit = 21249.99m, MonthlySalaryCredit = 21000, EmployeeShare = 945.00m, EmployerShare = 1930.00m + 140.00m },
        //        new SssMonthlyCredit { Id = 36, LowerLimit = 21250, UpperLimit = 21749.99m, MonthlySalaryCredit = 21500, EmployeeShare = 967.50m, EmployerShare = 1930.00m + 210.00m },
        //        new SssMonthlyCredit { Id = 37, LowerLimit = 21750, UpperLimit = 22249.99m, MonthlySalaryCredit = 22000, EmployeeShare = 990.00m, EmployerShare = 1930.00m + 280.00m },
        //        new SssMonthlyCredit { Id = 38, LowerLimit = 22250, UpperLimit = 22749.99m, MonthlySalaryCredit = 22500, EmployeeShare = 1012.50m, EmployerShare = 1930.00m + 350.00m },
        //        new SssMonthlyCredit { Id = 39, LowerLimit = 22750, UpperLimit = 23249.99m, MonthlySalaryCredit = 23000, EmployeeShare = 1035.00m, EmployerShare = 1930.00m + 420.00m },
        //        new SssMonthlyCredit { Id = 40, LowerLimit = 23250, UpperLimit = 23749.99m, MonthlySalaryCredit = 23500, EmployeeShare = 1057.50m, EmployerShare = 1930.00m + 490.00m },
        //        new SssMonthlyCredit { Id = 41, LowerLimit = 23750, UpperLimit = 24249.99m, MonthlySalaryCredit = 24000, EmployeeShare = 1080.00m, EmployerShare = 1930.00m + 560.00m },
        //        new SssMonthlyCredit { Id = 42, LowerLimit = 24250, UpperLimit = 24749.99m, MonthlySalaryCredit = 24500, EmployeeShare = 1102.50m, EmployerShare = 1930.00m + 630.00m },
        //        new SssMonthlyCredit { Id = 43, LowerLimit = 24750, UpperLimit = 25249.99m, MonthlySalaryCredit = 25000, EmployeeShare = 1125.00m, EmployerShare = 1930.00m + 700.00m },
        //        new SssMonthlyCredit { Id = 44, LowerLimit = 25250, UpperLimit = 25749.99m, MonthlySalaryCredit = 25500, EmployeeShare = 1147.50m, EmployerShare = 1930.00m + 770.00m },
        //        new SssMonthlyCredit { Id = 45, LowerLimit = 25750, UpperLimit = 26249.99m, MonthlySalaryCredit = 26000, EmployeeShare = 1170.00m, EmployerShare = 1930.00m + 840.00m },
        //        new SssMonthlyCredit { Id = 46, LowerLimit = 26250, UpperLimit = 26749.99m, MonthlySalaryCredit = 26500, EmployeeShare = 1192.50m, EmployerShare = 1930.00m + 910.00m },
        //        new SssMonthlyCredit { Id = 47, LowerLimit = 26750, UpperLimit = 27249.99m, MonthlySalaryCredit = 27000, EmployeeShare = 1215.00m, EmployerShare = 1930.00m + 980.00m },
        //        new SssMonthlyCredit { Id = 48, LowerLimit = 27250, UpperLimit = 27749.99m, MonthlySalaryCredit = 27500, EmployeeShare = 1237.50m, EmployerShare = 1930.00m + 1050.00m },
        //        new SssMonthlyCredit { Id = 49, LowerLimit = 27750, UpperLimit = 28249.99m, MonthlySalaryCredit = 28000, EmployeeShare = 1260.00m, EmployerShare = 1930.00m + 1120.00m },
        //        new SssMonthlyCredit { Id = 50, LowerLimit = 28250, UpperLimit = 28749.99m, MonthlySalaryCredit = 28500, EmployeeShare = 1282.50m, EmployerShare = 1930.00m + 1190.00m },
        //        new SssMonthlyCredit { Id = 51, LowerLimit = 28750, UpperLimit = 29249.99m, MonthlySalaryCredit = 29000, EmployeeShare = 1305.00m, EmployerShare = 1930.00m + 1260.00m },
        //        new SssMonthlyCredit { Id = 52, LowerLimit = 29250, UpperLimit = decimal.MaxValue, MonthlySalaryCredit = 30000, EmployeeShare = 1350.00m, EmployerShare = 1930.00m + 1400.00m }
        //    );
        //}
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
