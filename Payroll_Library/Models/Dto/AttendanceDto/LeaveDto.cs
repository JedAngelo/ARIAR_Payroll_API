using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll_Library.Models.Dto.EmployeeDto;

namespace Payroll_Library.Models.Dto.AttendanceDto
{
    public class LeaveDto
    {
        public int LeaveId { get; set; }

        public Guid PersonalId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;
    }
}
