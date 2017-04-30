using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Robert Forbes
    /// 
    /// Created:
    /// 2017/03/24
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// 
    /// </remarks>
    public class RepairLine
    {
        public string RepairDescription { get; set; }
        public int? RepairId { get; set; }
        public int? RepairLineId { get; set; }


        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/03/24
        /// </summary>
        /// 
        /// <remarks>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// 
        /// Standardized Comment
        /// 
        /// </remarks>
        /// <returns>Repair Description To string</returns>
        public override string ToString()
        {
            return RepairDescription;
        }
    }
}
