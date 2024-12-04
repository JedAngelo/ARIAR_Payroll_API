using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class NetSalary
{
    public int NetSalaryId { get; set; }

    public Guid PayrollId { get; set; }

    public decimal NetSalaryAmount { get; set; }

    public virtual Payroll Payroll { get; set; } = null!;
}
