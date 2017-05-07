using System;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name="Supplier Product Lot ID")]
        public int SupplierProductLotId { get; set; }

        [Display(Name="Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name="Product ID")]
        public int ProductId { get; set; }

        [Display(Name="Quantity")]
        public int Quantity { get; set; }

        [Display(Name="Supplier ID")]
        public int SupplierId { get; set; }

        [Display(Name="Price")]
        public decimal? Price { get; set; }
    }
}