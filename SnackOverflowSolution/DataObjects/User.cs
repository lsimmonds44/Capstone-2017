using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
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
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage ="Invalid Zip Code")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public String EmailAddress { get; set; }

        public bool EmailPreferences { get; set; }

        [Required]
        public String UserName { get; set; }

        public bool Active { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

    }
}
