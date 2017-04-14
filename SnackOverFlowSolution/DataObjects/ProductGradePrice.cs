using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ProductGradePrice
    {
        public int ProductID { get; set; }
        public string GradeID { get; set; }
        public decimal Price { get; set; }
    }
}
