using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// DTO for Commercial Invoice
    /// </summary>
    public class CommercialInvoice
    {
        public int CommercialInvoiceId { get; set; }
        public int CommercialId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public decimal AmountPaid { get; set; }
        public bool Approved { get; set; }
        public bool Active { get; set; }
    }
}
