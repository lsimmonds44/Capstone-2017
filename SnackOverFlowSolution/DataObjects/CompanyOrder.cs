using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// 2017/04/13
    /// </summary>
    public class CompanyOrder
    {
        public int CompanyOrderID { get; set; }
        public int EmployeeId { get; set; }
        public int SupplierId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public bool HasArrived { get; set; }
        public bool  Active { get; set; }
    }
}
