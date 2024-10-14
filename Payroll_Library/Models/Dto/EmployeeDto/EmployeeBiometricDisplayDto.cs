using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.EmployeeDto
{
    public class EmployeeBiometricDisplayDto
    {
        public Guid PersonalId { get; set; }

        public byte[] BiometricData { get; set; } = null!;
    }
}
