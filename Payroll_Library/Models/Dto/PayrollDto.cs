using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class PayrollDto
    {
        public Guid PayrollId { get; set; }

        public Guid PersonalId { get; set; }

        public decimal TotalWorkDay { get; set; }

        public DateOnly PaymentDate { get; set; }

        public virtual List<GrossSalaryDto> GrossSalaryDtos { get; set; } = new List<GrossSalaryDto>();

        public virtual List<NetSalaryDto> NetSalaryDtos { get; set; } = new List<NetSalaryDto>();

        //public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;

        public virtual List<TotalDeductionsDto> TotalDeductionsDto { get; set; } = new List<TotalDeductionsDto>();
    }
}
