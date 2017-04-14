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
        public decimal? pricePaid { get; set; }
        public int? quantity { get; set; }
        public int? productLotId { get; set; }
        public int? packageId { get; set; }
        public int? packageLineId { get; set; }
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
            return "Lot ID: " + productLotId + " - Quantity: " + quantity;
        }
    }
}
