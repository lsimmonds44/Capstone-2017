using System.Collections.Generic;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Michael Takrama
    /// 
    /// Created:
    /// 2017/04/07
    /// 
    /// Skyler Hiscock
    /// Updated:
    /// 2017/04/20
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
    /// Skyler Hiscock
    /// 
    /// Created:
    /// 2017/04/20
    /// </summary>

    public class NavMenuViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public string SearchPhrase { get; set; }
        public string SelectedCategory { get; set; }
    }
}