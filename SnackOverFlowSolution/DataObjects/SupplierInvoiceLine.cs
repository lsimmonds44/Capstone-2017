using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// Created:
    /// 
    /// 2017/03/22
    /// 
    /// DTO for the Supplier Invoice Line, just one line on a Supplier Invoice
    /// </summary>
    public class SupplierInvoiceLine
    {
        public int SupplierInvoiceId { get; set; }
        public int ProductLotId { get; set; }
        public int QuantitySold { get; set; }
        public decimal PriceEach { get; set; }
        public decimal ItemDiscount { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
