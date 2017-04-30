using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Skyler Hiscock
    /// 
    /// Created:
    /// 2017/04/15
    /// 
    /// Author: Adam Freeman (book: Pro ASP.NET MVC 5)
    /// </summary>
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
}