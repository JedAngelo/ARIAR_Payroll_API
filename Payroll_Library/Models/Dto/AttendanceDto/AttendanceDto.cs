using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.AttendanceDto
{
    public class AttendanceDto
    {
        public int? AttendanceId { get; set; }

        public Guid PersonalId { get; set; }

        public TimeOnly? MorningIn { get; set; }

        public TimeOnly? MorningOut { get; set; }

        public TimeOnly? AfternoonIn { get; set; }

        public TimeOnly? AfternoonOut { get; set; }

        public DateOnly? AttendanceDate { get; set; }

        public string? Status { get; set; }

        //public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;
    }
}
