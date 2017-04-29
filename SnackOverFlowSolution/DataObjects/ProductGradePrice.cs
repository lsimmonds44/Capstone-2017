using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Updated:
    /// 2017/04/29
    /// 
    /// Product Grade Price Class
    /// </summary>

    public class ProductGradePrice
    {
        public int ProductID { get; set; }
        public string GradeID { get; set; }
        public decimal Price { get; set; }
    }
}
