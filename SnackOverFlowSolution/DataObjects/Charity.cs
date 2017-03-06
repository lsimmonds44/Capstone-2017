using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Charity
    {
        public int CharityID { get; set; }
        public int UserID { get; set; }
        public int EmployeeID { get; set; }
        public String CharityName { get; set; }
        public String ContactFirstName { get; set; }
        public String ContactLastName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String ContactHours { get; set; }
        public String Status { get; set; }

    }
}
