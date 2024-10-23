using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.AttendanceDto
{
    public class AttendanceDisplayDto
    {
        public string? Name { get; set; }
        public TimeOnly? Log { get; set; }
        public string? Type { get; set; }
    }
}
