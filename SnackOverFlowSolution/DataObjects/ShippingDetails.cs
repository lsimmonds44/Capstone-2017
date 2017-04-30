using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataObjects
{
    /// <summary>
    /// Michael Takrama
    /// 
    /// Created:
    /// 2017/04/17
    /// 
    /// Shipping Details
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// </remarks>
    public class ShippingDetails
    {

        [Required(ErrorMessage = "Please enter the first address line")]
        [Display(Name = "Address Line 1")]
        public string Line1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        public string Zip { get; set; }

        public int CustomerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string IdentityUsername { get; set; }
    }
}