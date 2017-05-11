using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int? EmployeeId { get; set; }
        public int? UserId { get; set; }
        public decimal? Salary { get; set; }
        public bool? Active { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string EmployeeName { get; set; }

    }
}
