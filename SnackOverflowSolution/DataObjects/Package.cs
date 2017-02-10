///<summary>
///Robert Forbes
///2017/02/02
///
/// 
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Package
    {

        //The delivery id is a nullable int s as it can be null in the database
        public int? deliveryId { get; set; }
        public int orderId { get; set; }
        public int packageId { get; set; }
        public List<PackageLine> packageLineList { get; set; }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// overrides the default ToString method
        /// </summary>
        /// <returns>a string representation of the package</returns>
        public override string ToString()
        {
            return "Package: " + packageId;
        }

    }
}
