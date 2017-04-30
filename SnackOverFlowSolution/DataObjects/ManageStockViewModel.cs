using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Michael Takrama
    /// Created:
    /// 2017/03/02
    /// 
    /// View Model for frmManageStock Datagrid
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// </remarks>
    public class ManageStockViewModel
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        public string SupplierName { get; set; }

        public int? LocationId { get; set; }

        public string LocationDesc { get; set; }

        public int? Quantity { get; set; }

        public int? AvailableQuantity { get; set; }

        public int? ProductLotId { get; set; }
    }
}
