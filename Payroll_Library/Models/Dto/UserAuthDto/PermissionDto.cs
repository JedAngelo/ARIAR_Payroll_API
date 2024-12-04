using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.UserAuthDto
{
    public class PermissionDto
    {
        public int PermissionId { get; set; }
        public string? AccessView { get; set; }
        public string? UserId { get; set; }
        //public ApplicationUser? User { get; set; }
        public UsersDto? User { get; set; }
    }
}
