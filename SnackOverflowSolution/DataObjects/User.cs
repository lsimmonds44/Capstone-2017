using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Phone { get; set; }
        public int PreferredAddressId { get; set; }
        public String EmailAddress { get; set; }
        public bool EmailPreferences { get; set; }
        public String UserName { get; set; }
        public bool Active { get; set; }

    }
}
