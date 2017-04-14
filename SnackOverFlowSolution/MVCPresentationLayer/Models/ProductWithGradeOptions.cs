using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    public class ProductWithGradeOptions
    {
        public Product Product { get; set; }
        public IEnumerable<ProductGradePrice> GradeOptions { get; set; }
    }
}