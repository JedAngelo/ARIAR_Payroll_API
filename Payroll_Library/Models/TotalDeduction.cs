using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Library.Models;

public partial class TotalDeduction
{
    [Key]
    public int TotalDeductionId { get; set; }

    public Guid PayrollId { get; set; }

    public int PhilhealthDeductionAmount { get; set; }

    public int PagibigDeductionAmount { get; set; }

    public int SssDeductionAmount { get; set; }

    public int IncomeTaxDeductionAmount { get; set; }

    public virtual Payroll Payroll { get; set; } = null!;
}
