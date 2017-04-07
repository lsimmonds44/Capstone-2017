using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Created by William Flood on 2017-04-07
    /// </summary>
    public class UserCartLine
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public String GradeID { get; set; }
        public String Name { get; set; }
        public decimal Total { get; set; }
    }
}
