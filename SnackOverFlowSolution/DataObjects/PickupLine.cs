using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Robert Forbes
    /// 2017/04/13
    /// </summary>
    public class PickupLine
    {
        public bool? PickupStatus { get; set; }
        public int? Quantity { get; set; }
        public int? ProductId { get; set; }
        public int? PickupId { get; set; }
        public int? PickupLineId { get; set; }

        public string productName { get; set; }
    }
}
