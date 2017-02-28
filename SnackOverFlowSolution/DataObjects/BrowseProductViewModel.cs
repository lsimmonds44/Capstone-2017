using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Created by Natacha Ilunga
    /// Created on 2-20-2017 
    ///
    /// View model for frmBrowseProducts
    /// </summary>
    public class BrowseProductViewModel
    {
        private string SupplierNameToView;

        private string Category_ID;

        public int? ProductId { get; set; }

        public byte[] Image_Binary { get; set; }

        public string SourceString { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GradeID { get; set; }

        public string CategoryID
        {
            get { return Category_ID == "" ? "Uncategorized" : Category_ID; }
            set { Category_ID = value; }
        }

        public double Price { get; set; }

        public string PriceString
        {
            get { return Price == Double.MinValue ? "N/A" : Price.ToString("c"); }
        }

        public int? SupplierID { get; set; }

        public string Supplier_Name
        {
            get
            {
                return SupplierNameToView == "" ? "No Vendor" : SupplierNameToView;
            }
            set { SupplierNameToView = value; }
        }

    }
}
