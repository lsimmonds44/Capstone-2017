using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Supplier
    {
        // Represents a Supplier
        // Created by Christian Lopez on 2017/02/01
        // Last modified by Christian Lopez on 2017/02/02
        public int SupplierID { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public String FarmTaxID { get; set; }
        public int UserId { get; set; }
        public string FarmName { get; set; }
        public string FarmAddress { get; set; }
        public string FarmCity { get; set; }
        public string FarmState { get; set; }
        public bool Active { get; set; }

        /// <summary>
        /// Skyler Hiscock
        /// created 2017/03/09
        /// 
        /// Returns clone of Supplier
        /// </summary>
        /// <returns>Supplier</returns>
        public Supplier Clone()
        {
            return new Supplier{
                SupplierID = this.SupplierID,
                IsApproved = this.IsApproved,
                ApprovedBy = this.ApprovedBy,
                FarmTaxID = this.FarmTaxID,
                UserId = this.UserId,
                FarmName = this.FarmName,
                FarmAddress = this.FarmAddress,
                FarmCity = this.FarmCity,
                FarmState = this.FarmState,
                Active = this.Active
            };
        }
    }
}
