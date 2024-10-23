using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Library.Models;

public partial class GrossSalary
{
    [Key]
    public int SalaryId { get; set; }

    public Guid PayrollId { get; set; }

    public decimal GrossSalaryAmount { get; set; }

    public virtual Payroll Payroll { get; set; } = null!;
}
