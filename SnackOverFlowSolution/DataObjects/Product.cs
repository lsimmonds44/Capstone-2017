using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
<<<<<<< HEAD
    public class Product
    {
        public int ProductID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public decimal UnitPrice { get; set; }
        public String ImageName { get; set; }
        public String UnitOfMeasurement { get; set; }
        public decimal DeliveryChargePerUnity { get; set; }
=======
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
>>>>>>> 8f8fd8df866ad3ea16b5e01e1ff3c693f4310771
    }
}
