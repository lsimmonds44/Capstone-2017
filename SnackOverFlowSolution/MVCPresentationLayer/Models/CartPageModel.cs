using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentationLayer.Models
{
    public class CartPageModel
    {
        public List<int> SavedOrderList { get; set; }
        public List<UserCartLine> ItemsInCart { get; set; }
    }
}