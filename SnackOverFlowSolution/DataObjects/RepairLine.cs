using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Robert Forbes
    /// 2017/03/24
    /// </summary>
    public class RepairLine
    {
        public string RepairDescription { get; set; }
        public int? RepairId { get; set; }
        public int? RepairLineId { get; set; }


        public override string ToString()
        {
            return RepairDescription;
        }
    }
}
