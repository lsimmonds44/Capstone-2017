using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class CommercialCustomer
    {
        public int Commercial_Id { get; set; }
        public int User_Id { get; set; }
        public bool IsApproved { get; set; }
        public int ApprovedBy { get; set; }
        public int FedTaxId { get; set; }
        public bool Active { get; set; }
    }
}
