using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataObjects
{
    /// <summary>
    /// Christian Lopez
    /// 
    /// Created:
    /// 2017/02/15
    /// 
    /// The DTO for Product
    /// 
    /// </summary>
    /// <remarks>
    /// Michael Takrama 
    /// Updated:
    /// 2017/04/07
    /// 
    /// Detail - Added Data Annotation for Model Fields
    /// 
    /// Ariel Sigo 
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// 
    /// </remarks>
    public class Product
    {

        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        [Required(ErrorMessage="Please enter a product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Michael Takrama
        /// Created:
        /// 2017/04/07
        /// 
        /// Price Property - MVC Layer
        /// </summary>
        [Required]
        [Range(0.02, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public double Price { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageName { get; set; }
        public bool? Active { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal DeliveryChargePerUnit { get; set; }
        public byte[] ImageBinary { get; set; }
        public string GradeId { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
