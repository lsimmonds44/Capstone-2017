using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }
        public int ProductOrderID { get; set; }
        public int? ProductID { get; set; }
        public int Quantity { get; set; }
        public String GradeID { get; set; }
        public decimal Price { get; set; }
        public decimal UnitDiscount { get; set; }
    }
}
