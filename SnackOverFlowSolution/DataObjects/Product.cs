using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/15
    /// 
    /// The DTO for Product
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageName { get; set; }
        public bool Active { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal DeliveryChargePerUnit { get; set; }
    }
}
