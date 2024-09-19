using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public Guid PersonalId { get; set; }

    public DateTime MorningIn { get; set; }

    public DateTime MorningOut { get; set; }

    public DateTime AfternoonIn { get; set; }

    public DateTime AfternoonOut { get; set; }

    public virtual PersonalInformation Personal { get; set; } = null!;
}
