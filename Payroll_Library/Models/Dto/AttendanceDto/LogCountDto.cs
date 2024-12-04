using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.AttendanceDto
{
    public class LogCountDto
    {
        public int? PresentCount { get; set; }
        public int? AbsentCount { get; set; }
        public int? LeaveCount { get; set; }
    }
}
