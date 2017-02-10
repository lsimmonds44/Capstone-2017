using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace DataObjects
{
    public class BackorderPreorder
    {
        public int? BackorderPreorderId { get; set; }
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DatePlaced { get; set; }
        public DateTime? DateExpected { get; set; }
        public bool? HasArrived { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
    }
}
