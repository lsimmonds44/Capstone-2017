using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
 
    /// <summary>
    /// Christian Lopez
    /// Created:
    /// 2017/02/01
    /// 
    /// Represents a Supplier
    /// </summary>
    /// <remarks>
    /// Christian Lopez
    /// Updated:
    /// 2017/04/06
    /// Added Data Annotations
    /// 
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standarized Comment
    /// </remarks>
    public class Supplier
    {
        
        public int SupplierID { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        [Required(ErrorMessage="You must supply a Farm Tax ID")]
        [StringLength(64, ErrorMessage="Cannot be longer than 64 characters")]
        [Display(Name="Farm Tax ID")]
        public String FarmTaxID { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage="You must supply a Farm Name")]
        [StringLength(300, ErrorMessage="Cannot be longer than 300 characters")]
        [Display(Name="Farm Name")]
        public string FarmName { get; set; }
        [Required(ErrorMessage = "You must supply a Farm Address")]
        [StringLength(300, ErrorMessage = "Cannot be longer than 300 characters")]
        [Display(Name = "Farm Address")]
        public string FarmAddress { get; set; }
        [Required(ErrorMessage = "You must supply a Farm City")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters")]
        [Display(Name = "Farm City")]
        public string FarmCity { get; set; }
        [Required(ErrorMessage = "You must supply a Farm State Abbreviation")]
        [StringLength(2, ErrorMessage = "Cannot be longer than 2 characters")]
        [MinLength(2, ErrorMessage="Must be exactly 2 characters")]
        [Display(Name = "Farm State Abbreviation")]
        public string FarmState { get; set; }
        public bool Active { get; set; }

        /// <summary>
        /// Skyler Hiscock
        /// created:
        /// 2017/03/09
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

        /// <summary>
        /// Ariel Sigo
        /// Updated: 
        /// 2017/04/29
        /// </summary>
        /// <returns>FarmName in a string format</returns>
        public override string ToString()
        {
            return FarmName;
        }
    }
}
