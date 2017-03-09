using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// Created 2017/03/08
    /// 
    /// Represents the DTO for an Agreement
    /// </summary>
    public class Agreement
    {
        public int AgreementId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
    }
}
