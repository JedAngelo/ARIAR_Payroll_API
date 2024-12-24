using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.PayrollDto
{
    public class PayrollDto
    {
        public Guid? PayrollId { get; set; }

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


        //public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;

    }
}
