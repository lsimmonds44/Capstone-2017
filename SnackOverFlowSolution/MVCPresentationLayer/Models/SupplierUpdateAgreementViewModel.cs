using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Ariel Sigo 
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Combines Supplier and Aggreement for View
    /// </summary>
    public class SupplierUpdateAgreementViewModel
    {
        public int SupplierId { get; set; }
        public List<AgreementWithProductName> ApprovedAgreements { get; set; }
        [Display(Name="Products for Agreement")]
        public List<Product> ProductsToSelect { get; set; }
        public int[] ProductIDs { get; set; }
    }
}