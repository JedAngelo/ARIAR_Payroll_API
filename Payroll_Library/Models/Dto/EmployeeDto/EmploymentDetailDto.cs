using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.EmployeeDto
{
    public class EmploymentDetailDto
    {
        public int EmploymentId { get; set; }

        public Guid PersonalId { get; set; }

        public int PositionId { get; set; }

        public DateTime HireDate { get; set; }

        public decimal PayRate { get; set; }


        public decimal PhilhealthEmployeeRate { get; set; }

        public decimal SssEmployeeRate { get; set; }

        public decimal PagibigEmployeeRate { get; set; }

        public decimal IncomeTaxRate { get; set; }

        //public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;

        //public virtual PostionDto PositionDtos { get; set; } = new PostionDto();
    }
}
