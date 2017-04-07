using System.Web.Mvc;
using LogicLayer;

namespace MVCPresentationLayer.Infrastructure
{
    /// <summary>
    /// Created by Michael Takrama
    /// 04/07/2017
    /// 
    /// ModelBinder for Cart in CartController
    /// </summary>
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart form the session

            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }
            //create the cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            // return the cart
            return cart;
        }
    }
}