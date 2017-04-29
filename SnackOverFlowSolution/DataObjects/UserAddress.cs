using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// Created:
    /// 2017/02/01
    /// 
    /// Represents a User Address
    /// </summary>
    /// <remarks>
    /// Christian Lopez
    /// Updated:
    /// 2017/02/01
    /// 
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// 
    /// </remarks>
    public class UserAddress
    {

        public int UserAddressId { get; set; }
        public int UserId { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
