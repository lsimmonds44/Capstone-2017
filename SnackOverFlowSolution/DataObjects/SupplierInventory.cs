using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// William Flood
    /// Created:
    /// 2017/03/29
    /// 
    /// Class for Supplier Inventory
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// </remarks>
    public class SupplierInventory
    {
        public int? SupplierInventoryID { get; set; }
        public int? AgreementID { get; set; }
        public int? Quantity { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
