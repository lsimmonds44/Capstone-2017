using System.Collections.Generic;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Created by Michael Takrama
    /// 04/07/2017
    /// 
    /// Composite Data Object View Model for Product/Index
    /// </summary>

    public class ProductsListViewModel
    {
        public List<BrowseProductViewModel> Products { get; set; }

    }
}