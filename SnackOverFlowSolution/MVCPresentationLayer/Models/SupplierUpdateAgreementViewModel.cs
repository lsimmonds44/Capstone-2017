using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.Models
{
    public class SupplierUpdateAgreementViewModel
    {
        public int SupplierId { get; set; }
        public List<AgreementWithProductName> ApprovedAgreements { get; set; }
        [Display(Name="Products for Agreement")]
        public List<Product> ProductsToSelect { get; set; }
        public int[] ProductIDs { get; set; }
    }
}