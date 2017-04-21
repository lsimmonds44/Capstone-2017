///<summary>
///Robert Forbes
///2017/02/02
///
///</summary>
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PackageLine
    {
        public decimal? PricePaid { get; set; }
        public int? Quantity { get; set; }
        public int? ProductLotId { get; set; }
        public int? PackageId { get; set; }
        public int? PackageLineId { get; set; }
        public string ProductName { get; set; }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// overrides the default ToString method
        /// </summary>
        /// <returns>a string representation of the package line</returns>
        public override string ToString()
        {
            return "Lot ID: " + ProductLotId + " - Quantity: " + Quantity;
        }
    }
}
