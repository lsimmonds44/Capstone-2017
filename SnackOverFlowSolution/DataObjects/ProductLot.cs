using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// 
    /// Created:
    /// 2017/02/20
    /// 
    /// The DTO for a Product Lot
    /// </summary>
    public class ProductLot
    {
        public int? ProductLotId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? LocationId { get; set; }
        public int? ProductId { get; set; }
        public int? SupplyManagerId { get; set; }
        public int? Quantity { get; set; }
        public int? AvailableQuantity { get; set; }
        public String Grade { get; set; }
        public Decimal? Price { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public String ProductName { get; set; }
    }
}
