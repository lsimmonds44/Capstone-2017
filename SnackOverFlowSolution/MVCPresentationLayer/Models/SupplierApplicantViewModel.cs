using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Christian Lopez
    /// 2017/04/19
    /// 
    /// Combines User and Supplier information for View
    /// </summary>
    public class SupplierApplicantViewModel
    {

        //User
        [Key]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Not a valid Phone number")]
        public String Phone { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLineOne { get; set; }

        
        [Display(Name = "Address Line 2")]
        public string AddressLineTwo { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public String EmailAddress { get; set; }

        public bool EmailPreferences { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage="Username cannot be more than 20 characters")]
        public String UserName { get; set; }

        public bool Active { get; set; }

        [Required]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage="Passwords must be a combination of letters, digits, and special characters.")]
        public string Password { get; set; }
        [Required]
        [CompareAttribute("Password",ErrorMessage="Passwords do not match")]
        public string ConfirmPassword { get; set; }

        //Supplier
        public int SupplierID { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        [Required(ErrorMessage = "You must supply a Farm Tax ID")]
        [StringLength(64, ErrorMessage = "Cannot be longer than 64 characters")]
        [Display(Name = "Farm Tax ID")]
        public String FarmTaxID { get; set; }
        //public int UserId { get; set; }
        [Required(ErrorMessage = "You must supply a Farm Name")]
        [StringLength(300, ErrorMessage = "Cannot be longer than 300 characters")]
        [Display(Name = "Farm Name")]
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
        [MinLength(2, ErrorMessage = "Must be exactly 2 characters")]
        [Display(Name = "Farm State Abbreviation")]
        public string FarmState { get; set; }
        //public bool Active { get; set; }

        public List<AgreementWithProductName> Agreements { get; set; }

        public int[] ProductIDs { get; set; }


    }
}