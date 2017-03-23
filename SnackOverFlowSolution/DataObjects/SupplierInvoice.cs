using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// 2017/03/22
    /// 
    /// The DTO for a Supplier Invoice
    /// </summary>
    public class SupplierInvoice
    {
        public int SupplierInvoiceId { get; set; }
        public int SupplierId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public decimal AmountPaid { get; set; }
        public bool Active { get; set; }
        
    }
}
