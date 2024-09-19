using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
