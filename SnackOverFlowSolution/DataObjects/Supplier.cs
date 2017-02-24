using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public int UserID { get; set; }
        public bool IsApproved { get; set; }
        public int ApprovedBy { get; set; }
        public String FarmTaxID { get; set; }
    }
}
