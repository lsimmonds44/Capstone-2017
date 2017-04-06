using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// 2017/04/06
    /// 
    /// Exists for MVC Modeling
    /// </summary>
    public class SupplierWithAgreements : Supplier
    {
        public List<Agreement> Agreements { get; set; }
    }
}
