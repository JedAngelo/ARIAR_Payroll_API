using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class EmploymentDetailDto
    {
        public int EmploymentId { get; set; }

        public Guid PersonalId { get; set; }

        public int PositionId { get; set; }

        public DateOnly HireDate { get; set; }

        public decimal PayRate { get; set; }

        public string Status { get; set; } = null!;

        public decimal PhilhealthEmployeeRate { get; set; }

        public decimal SssEmployeeRate { get; set; }

        public decimal PagibigEmployeeRate { get; set; }

        public decimal IncomeTaxRate { get; set; }

        public virtual PersonalInformationDto PersonalDto { get; set; } = null!;

        public virtual Position PositionDtos { get; set; } = null!;
    }
}
