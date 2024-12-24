using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.PayrollDto
{
    public class NetSalaryDto
    {
        public int NetSalaryId { get; set; }

        public Guid PayrollId { get; set; }

        public decimal NetSalaryAmount { get; set; }

        public virtual PayrollDto? PayrollDtos { get; set; } = null!;
    }
}
