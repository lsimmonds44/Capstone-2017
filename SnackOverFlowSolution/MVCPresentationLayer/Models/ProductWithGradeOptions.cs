using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    public class ProductWithGradeOptions
    {
        public Product product { get; set; }
        public IEnumerable<ProductGradePrice> gradeOptions { get; set; }
    }
}