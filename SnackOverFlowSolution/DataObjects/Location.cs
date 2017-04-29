using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Udpated:
    /// 2017/04/29
    /// 
    /// The DTO for the location
    /// </summary>
    public class Location
    {
        public int LocationId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
