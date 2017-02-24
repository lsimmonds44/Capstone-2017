using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Robert Forbes
    /// 2017/02/16
    /// Beginning of the product lot class, lists are not currently included as that would require the DTOs for thos objects
    /// And Im sure someone else is working on a full version
    /// </summary>
    public class ProductLot
    {
        public DateTime expirationDate { get; set; }
        public DateTime dateRecieved { get; set; }
        public int? availableQuantity { get; set; }
        public int? quantity { get; set; }
        public int? supplyManagerId { get; set; }
        public int? productId { get; set; }
        public int? supplierId { get; set; }
        public int? locationId { get; set; }
        public int? warehouseId { get; set; }
        public int? productLotId { get; set; }
    }
}
