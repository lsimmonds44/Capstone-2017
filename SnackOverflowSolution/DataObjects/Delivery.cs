using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace DataObjects
{
    public class Delivery
    {
        public int? DeliveryId { get; set; }
        public int? RouteId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public byte[] Verification { get; set; }
        public String StatusId { get; set; }
        public String DeliveryTypeId { get; set; }
        public int? OrderId { get; set; }
        public List<Package> PackageList { get; set; }

        public UserAddress Address { get; set; }

        /// <summary>
        /// Robert Forbes
        /// 2017/05/04
        /// 
        /// overrides the default ToString method
        /// </summary>
        /// <returns>a string representation of the delivery</returns>
        public override string ToString()
        {
            try
            {
                return Address.AddressLineOne + ", " + Address.City + ", " + Address.State;
            }
            catch
            {
                return DeliveryId.ToString();
            }
            
        }
    }
}