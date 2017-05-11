using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name="Commercial Invoice ID")]
        public int CommercialInvoiceId { get; set; }

        [Display(Name = "Commercial ID")]
        public int CommercialId { get; set; }

        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }

        public bool Approved { get; set; }

        public bool Active { get; set; }

        public int OrderId { get; set; }

        public bool Paid { get; set; }
        
    }
}
