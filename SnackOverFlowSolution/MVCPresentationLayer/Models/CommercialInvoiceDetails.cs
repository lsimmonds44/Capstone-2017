using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentationLayer.Models
{
    public class CommercialInvoiceDetails
    {
        public CommercialInvoice CommercialInvoice { get; set; }
        public List<CommercialInvoiceLine> CommercialInvoiceLines { get; set; }
    }
}