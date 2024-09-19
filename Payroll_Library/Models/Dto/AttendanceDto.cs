using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class AttendanceDto
    {
        public int AttendanceId { get; set; }

        public Guid PersonalId { get; set; }

        public DateTime MorningIn { get; set; }

        public DateTime MorningOut { get; set; }

        public DateTime AfternoonIn { get; set; }

        public DateTime AfternoonOut { get; set; }

        public virtual PersonalInformationDto PersonalDtos { get; set; } = null!;
    }
}
