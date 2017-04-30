using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// Created:
    /// 2017/04/06
    /// 
    /// Exists for MVC Modeling
    /// </summary>
    public class SupplierWithAgreements : Supplier
    {
        public int ID
        {
            get
            {
                return SupplierID;
            }
            set
            {
                SupplierID = value;
            }
        }

        public List<AgreementWithProductName> Agreements { get; set; }

        public int[] ProductIDs { get; set; }
    }
}
