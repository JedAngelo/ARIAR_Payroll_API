using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Library.Models;

public partial class Leave
{
    [Key]
    public int LeaveId { get; set; }

    public Guid PersonalId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual PersonalInformation Personal { get; set; } = null!;
}
