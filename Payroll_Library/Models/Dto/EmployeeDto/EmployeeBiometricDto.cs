using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.EmployeeDto
{
    public class EmployeeBiometricDto
    {
        public int RecordId { get; set; }

        public Guid PersonalId { get; set; }

        public byte[] BiometricData { get; set; } = null!;

        public DateTime RecordDate { get; set; }

        //public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;
    }
}
