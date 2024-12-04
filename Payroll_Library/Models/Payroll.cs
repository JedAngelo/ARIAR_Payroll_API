using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class Payroll
{
    public Guid PayrollId { get; set; }

    public Guid PersonalId { get; set; }

    public decimal TotalWorkDay { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual GrossSalary? GrossSalary { get; set; }

    public virtual NetSalary? NetSalary { get; set; }

    public virtual PersonalInformation Personal { get; set; } = null!;

    public virtual TotalDeduction? TotalDeduction { get; set; }
}
