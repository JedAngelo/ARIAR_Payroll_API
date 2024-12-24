using System;
using System.Collections.Generic;

namespace Payroll_Library.Models;

public partial class Payroll
{
    public Guid PayrollId { get; set; }

    public Guid PersonalId { get; set; }

    public decimal TotalWorkDay { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal GrossSalary { get; set; }

    public decimal NetSalary { get; set; }

    public decimal EmployerSssShare { get; set; }

    public decimal EmployerPagibigShare { get; set; }

    public decimal EmployerPhilhealthShare { get; set; }

    public decimal EmployeeSssShare { get; set; }

    public decimal EmployeePagibigShare { get; set; }

    public decimal EmployeePhilhealthShare { get; set; }

    public decimal OtherDeductions { get; set; }

    public decimal Commissions { get; set; }

    public virtual PersonalInformation Personal { get; set; } = null!;

}
