using LogicLayer;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// ViewModel for Cart
    /// </summary>
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}