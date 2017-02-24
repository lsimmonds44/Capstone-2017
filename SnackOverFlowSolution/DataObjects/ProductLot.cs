using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ProductLot
    {
        public int ProductLotID { get; set; }
        public int WarehouseID { get; set; }
        public int SupplierID { get; set; }
        public int LocationID { get; set; }
        public int ProductID { get; set; }
        public int SupplyManagerID { get; set; }
        public int Quantity { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
