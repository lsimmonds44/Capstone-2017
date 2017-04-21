using System.Collections.Generic;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Created by Michael Takrama
    /// 04/07/2017
    /// Update by Skyler Hiscock
    /// 04/20/2017
    /// Composite Data Object View Model for Product/Index
    /// </summary>

    public class ProductsListViewModel
    {
        public IEnumerable<BrowseProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string SearchPhrase { get; set; }

        public string CurrentCategory { get; set; }

        public IEnumerable<string> Categories { get; set; }

    }

    /// <summary>
    /// Author: Skyler Hiscock
    /// Added: 4/20/17
    /// </summary>

    public class NavMenuViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public string SearchPhrase { get; set; }
        public string SelectedCategory { get; set; }
    }
}