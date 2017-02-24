using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class UserAddress
    {
        // Represents a User Address
        // Created by Christian Lopez on 2017/02/01
        // Last modified by Christian Lopez on 2017/02/01

        public int UserAddressId { get; set; }
        public int UserId { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
