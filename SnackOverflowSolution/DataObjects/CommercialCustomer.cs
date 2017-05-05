using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class CommercialCustomer
    {
        public int CommercialId { get; set; }
        public int UserId { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public int FederalTaxId { get; set; }
        public bool Active { get; set; }
        public string name { get; set; }
        public string ApprovedByName { get; set; }
    }
}
