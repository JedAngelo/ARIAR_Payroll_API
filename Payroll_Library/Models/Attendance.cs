using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Library.Models;

public partial class Attendance
{
    [Key]
    public int AttendanceId { get; set; }

    public Guid PersonalId { get; set; }

    public TimeOnly? MorningIn { get; set; }

    public TimeOnly? MorningOut { get; set; }

    public TimeOnly? AfternoonIn { get; set; }

    public TimeOnly? AfternoonOut { get; set; }

    public DateOnly? AttendanceDate { get; set; }
    public string? Status { get; set; }
    public virtual PersonalInformation Personnel { get; set; } = null!;
}
