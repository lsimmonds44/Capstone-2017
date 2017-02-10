using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ProductOrder
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string OrderTypeId { get; set; }
        public string AddressType { get; set; }
        public string DeliveryType { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateExpected { get; set; }
        public decimal Discount { get; set; }
        public string OrderStatusId { get; set; }
        public int UserAddressId { get; set; }
        public bool HasArrived { get; set; }
    }
}
