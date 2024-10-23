using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Library.Models;

public partial class Payroll
{
    [Key]
    public Guid PayrollId { get; set; }

    public Guid PersonalId { get; set; }

    public decimal TotalWorkDay { get; set; }

    public DateOnly PaymentDate { get; set; }
    public virtual PersonalInformation Personnel { get; set; } = null!;

    public virtual GrossSalary GrossSalaries { get; set; } = new GrossSalary();

    public virtual NetSalary NetSalaries { get; set; } = new NetSalary();

    public virtual TotalDeduction TotalDeductions { get; set; } = new TotalDeduction();
}
