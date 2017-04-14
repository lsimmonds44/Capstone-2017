using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// 2017/04/13
    /// </summary>
    public class CompanyOrder
    {
        [Display(Name = "Order ID")]
        public int CompanyOrderID { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }
        [Display(Name = "Supplier ID")]
        public int SupplierId { get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Arrived")]
        public bool HasArrived { get; set; }
        public bool  Active { get; set; }
    }
}
