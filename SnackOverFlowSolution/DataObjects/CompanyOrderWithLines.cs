using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary> 
    /// Christian Lopez 
    /// 
    /// Created:
    /// 2017/04/13
    /// 
    /// Used for MVC
    /// </summary>
    public class CompanyOrderWithLines : CompanyOrder
    {
        public int ID
        {
            get
            {
                return CompanyOrderID;
            }
            set
            {
                CompanyOrderID = value;
            }
        }

        public List<CompanyOrderLine> OrderLines { get; set; }
    }
}
