using System;

namespace DataObjects
{
    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Class of Supplier Product Lot
    /// </summary>
    public class SupplierProductLot
    {
        public int SupplierProductLotId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
        public decimal? Price { get; set; }
    }
}