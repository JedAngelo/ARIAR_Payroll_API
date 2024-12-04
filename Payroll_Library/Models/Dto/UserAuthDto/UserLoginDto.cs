using Payroll_Library.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Models.Dto.UserAuthDto
{
    public class UserLoginDto
    {
        public string userID { get; set; }
        public string UserName { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        public string Token { get; set; }

        public string UserToken { get; set; }
        //public string newRefreshToken { get; set; }
        public IList<string>? UserRole { get; set; }
        public List<string>? Permissions { get; set; }
    }
}
