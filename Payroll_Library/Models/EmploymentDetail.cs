using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class EmploymentDetail
{
    public int EmploymentId { get; set; }

    public Guid PersonalId { get; set; }

    public int PositionId { get; set; }

    public DateTime HireDate { get; set; }

    public decimal PayRate { get; set; }

    public decimal PhilhealthEmployeeRate { get; set; }

    public decimal SssEmployeeRate { get; set; }

    public decimal PagibigEmployeeRate { get; set; }

    public decimal IncomeTaxRate { get; set; }

    public virtual PersonalInformation Personal { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
