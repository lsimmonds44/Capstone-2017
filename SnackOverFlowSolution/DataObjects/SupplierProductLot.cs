using System;

namespace DataAccessLayer
{
    public class SupplierProductLot
    {
        public int SupplierProductLotId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
        public decimal Price { get; set; }
    }
}