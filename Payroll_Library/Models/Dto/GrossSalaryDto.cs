using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class GrossSalaryDto
    {
        public int SalaryId { get; set; }

        public Guid PayrollId { get; set; }

        public decimal GrossSalaryAmount { get; set; }

        public virtual PayrollDto PayrollDtos { get; set; } = null!;
    }
}
