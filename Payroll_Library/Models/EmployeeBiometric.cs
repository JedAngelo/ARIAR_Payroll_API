using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class EmployeeBiometric
{
    public int RecordId { get; set; }

    public Guid PersonalId { get; set; }

    public byte[] BiometricData { get; set; } = null!;

    public DateTime RecordDate { get; set; }

    public virtual PersonalInformation Personal { get; set; } = null!;
}
