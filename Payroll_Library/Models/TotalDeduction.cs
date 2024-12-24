using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class TotalDeduction
{
    public int TotalDeductionId { get; set; }

    public Guid PayrollId { get; set; }

    public decimal PhilhealthDeductionAmount { get; set; }

    public decimal PagibigDeductionAmount { get; set; }

    public decimal SssDeductionAmount { get; set; }

    public decimal IncomeTaxDeductionAmount { get; set; }

    public decimal LateDeductionAmount { get; set; }

    public decimal? OtherDeductionAmount { get; set; }

    public virtual Payroll Payroll { get; set; } = null!;
}
