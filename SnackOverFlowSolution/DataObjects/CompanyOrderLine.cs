using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// 2017/04/13
    /// </summary>
    public class CompanyOrderLine
    {
        [Key]
        [Column(Order=1)]
        [Display(Name="Order ID")]
        public int CompanyOrderID { get; set; }
        [Key]
        [Column(Order=2)]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}
