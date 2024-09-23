using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.PayrollDto
{
    public class TotalDeductionsDto
    {
        public int TotalDeductionId { get; set; }

        public Guid PayrollId { get; set; }

        public int PhilhealthDeductionAmount { get; set; }

        public int PagibigDeductionAmount { get; set; }

        public int SssDeductionAmount { get; set; }

        public int IncomeTaxDeductionAmount { get; set; }

        public virtual Payroll Payroll { get; set; } = null!;
    }
}
