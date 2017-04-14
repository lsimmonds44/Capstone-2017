using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Ethan Jorgensen
    /// 4/13/17
    /// The DTO for a supplier product lot
    /// </summary>
    public class SupplierProductLot
    {
        public int? SupplierProductLotId { get; set; }
        public int? SupplierId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public Decimal Price { get; set; }
    }
}
