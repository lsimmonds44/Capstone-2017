using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Daniel Brown
    /// 02/08/17
    /// 
    /// An Employee of the system
    /// </summary>
    public class Employee
    {
        public int? EmployeeId { get; set; }
        public int? UserId { get; set; }
        public decimal? Salary { get; set; }
        public bool? Active { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
