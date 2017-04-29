using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class RegisterCommercialViewModel : User
    {

        [Required]
        [MinLength(7, ErrorMessage = "Password must be at least 7 characters long!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{9}", ErrorMessage = "Not a valid Tax ID")]
        public string FederalTaxID { get; set; }
    }
}