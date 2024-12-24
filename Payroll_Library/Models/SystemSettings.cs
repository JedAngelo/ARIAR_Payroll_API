using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models
{
    public class SystemSettings
    {
        public int SettingsId { get; set; }

        // Payroll settings
        public decimal EmployerSssRate { get; set; }
        public decimal EmployerPhilhealthRate { get; set; }
        public decimal EmployerPagibigRate { get; set; }

        // General settings
        //Attendance type: "IN/OUT" or "FULL"
        public string AttendanceType { get; set; } = null!;
        public bool PasswordlessManualAttendance { get; set; }
        public TimeOnly LateStartTimeMorning { get; set; }
        public TimeOnly LateStartTimeAfternoon { get; set; }
        public TimeOnly EarlyOutEndsMorning { get; set; }
        public TimeOnly EarlyOutEndsAfternoon { get; set; }


        

    }
}
