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

        public decimal PhilhealthDeductionAmount { get; set; }

        public decimal PagibigDeductionAmount { get; set; }

        public decimal SssDeductionAmount { get; set; }

        public decimal IncomeTaxDeductionAmount { get; set; }

        public decimal LateDeductionAmount { get; set; }

        public decimal? OtherDeductionAmount { get; set; }

        public virtual PayrollDto? PayrollDtos { get; set; } = null!;
    }
}
