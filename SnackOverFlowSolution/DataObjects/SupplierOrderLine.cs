using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplierOrderLine
    {
        public int OrderLineID { get; set; }
        public int SupplierProductOrderID { get; set; }
        public int? ProductID { get; set; }
        public String ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
