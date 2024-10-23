using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Library.Models;

public partial class EmployeeBiometric
{
    [Key]
    public int RecordId { get; set; }

    public Guid PersonalId { get; set; }

    public byte[] BiometricData { get; set; } = null!;

    public DateTime RecordDate { get; set; }

    public virtual PersonalInformation Personnel { get; set; } = null!;
}
