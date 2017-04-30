using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// Created :
    /// 2017/02/16
    /// 
    /// The DTO for an Inspection
    /// </summary>
    public class Inspection
    {
        public int InspectionId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductLotId { get; set; }
        public string GradeId { get; set; }
        public DateTime DatePerformed { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
