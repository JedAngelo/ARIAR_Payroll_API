using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class PersonalInformation
{
    public Guid PersonalId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Suffix { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public string MaritalStatus { get; set; } = null!;

    public byte Age { get; set; }

    public byte[]? EmployeeImage { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ContactInformation? ContactInformation { get; set; }

    public virtual ICollection<EmployeeBiometric> EmployeeBiometrics { get; set; } = new List<EmployeeBiometric>();

    public virtual EmploymentDetail? EmploymentDetail { get; set; }

    public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
}
