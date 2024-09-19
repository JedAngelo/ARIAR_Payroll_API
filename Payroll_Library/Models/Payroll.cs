using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class Payroll
{
    public Guid PayrollId { get; set; }

    public Guid PersonalId { get; set; }

    public decimal TotalWorkDay { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual ICollection<GrossSalary> GrossSalaries { get; set; } = new List<GrossSalary>();

    public virtual ICollection<NetSalary> NetSalaries { get; set; } = new List<NetSalary>();

    public virtual PersonalInformation Personal { get; set; } = null!;

    public virtual ICollection<TotalDeduction> TotalDeductions { get; set; } = new List<TotalDeduction>();
}
