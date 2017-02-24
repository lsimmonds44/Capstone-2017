using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Product
    {
        public int ProductID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public decimal UnitPrice { get; set; }
        public String ImageName { get; set; }
        public String UnitOfMeasurement { get; set; }
        public decimal DeliveryChargePerUnity { get; set; }
    }
}
