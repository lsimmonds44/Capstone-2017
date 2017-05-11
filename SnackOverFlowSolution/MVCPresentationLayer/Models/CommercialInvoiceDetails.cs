using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Model of Commercial Invoice Details
    /// </summary>
    public class CommercialInvoiceDetails
    {
        public CommercialInvoice CommercialInvoice { get; set; }
        public List<CommercialInvoiceLine> CommercialInvoiceLines { get; set; }
    }
}