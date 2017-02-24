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
        public int? DeliveryId { get; set; }
        public int? OrderId { get; set; }
        public int PackageId { get; set; }
        public List<PackageLine> PackageLineList { get; set; }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// overrides the default ToString method
        /// </summary>
        /// <returns>a string representation of the package</returns>
        public override string ToString()
        {
            return "Package: " + PackageId;
        }

    }
}
