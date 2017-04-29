using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// William Flood 
    /// 
    /// Created;
    /// 2017/04/07
    /// 
    /// Class of User Cart Line
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// 
    /// </remarks>
    public class UserCartLine
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public String GradeID { get; set; }
        public String Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? FlatProductDiscount { get; set; }
        public decimal? ScaledProductDiscount { get; set; }
        public decimal? FlatCategoryDiscount { get; set; }
        public decimal? ScaledCategoryDiscount { get; set; }
        public decimal Total
        {
            get
            {
                return (decimal)BasePrice * (ScaledCategoryDiscount??1) * (ScaledProductDiscount??1) - (FlatProductDiscount??0) - (FlatCategoryDiscount??0);
            }
        }
    }
}
