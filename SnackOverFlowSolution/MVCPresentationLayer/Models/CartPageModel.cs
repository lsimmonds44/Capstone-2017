using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Model of Cart Page
    /// 
    /// </summary>
    public class CartPageModel
    {
        public List<int> SavedOrderList { get; set; }
        public List<UserCartLine> ItemsInCart { get; set; }
    }
}