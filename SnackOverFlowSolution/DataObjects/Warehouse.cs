using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Ariel Sigo
    /// Created:
    /// 2017/04/29
    /// 
    /// The DTO for Warehosue
    /// </summary>
    public class Warehouse
    {
        public int WarehouseID { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo {get;set;}
        public string City {get;set;}
        public string State {get;set;}
        public string Zip {get;set;}
    }
}
