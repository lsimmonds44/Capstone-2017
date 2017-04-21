using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Created by Michael Takrama
    /// 4/7/2017
    /// 
    /// Cart Controller
    /// </summary>
    public class CartController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICustomerOrderManager _customerOrderManager;
        private readonly IUserManager _userManager;
        private readonly IUserCartManager _userCartManager;

        public CartController(IProductManager repo, IUserManager _userManager, IUserCartManager _userCartManager)
        {
            _productManager = repo;
            this._userCartManager = _userCartManager;
            this._userManager = _userManager;
        }

        public CartController(IProductManager repo, ICustomerOrderManager proc, IUserManager _userManager, IUserCartManager _userCartManager)
        {
            _productManager = repo;
            _customerOrderManager = proc;
            this._userCartManager = _userCartManager;
            this._userManager = _userManager;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// William Flood
        /// 2017/04/14
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToRouteResult AddToCart(Cart cart, int? productId, string returnUrl)
        {
            try
            {
                var productViewModel = _productManager.RetrieveProductsToBrowseProducts().FirstOrDefault(p => p.ProductId == productId);

                var product = new Product
                {
                    ProductId = productViewModel.ProductId,
                    GradeId = productViewModel.GradeID,
                    Price = productViewModel.Price,
                    Name = productViewModel.Name
                };
                
                if (product.ProductId != 0)
                {
                    cart.AddItem(product, 1);
                    var currentUser = _userManager.RetrieveUserByUserName(User.Identity.Name);
                    var gradeId = Request.Params["Grade"];
                    
                    var quantity = Int32.Parse(Request.Params["Quantity"]);
                    var cartLine = new UserCartLine {
                        UserID = currentUser.UserId,
                        ProductID = (int)productId,
                        GradeID = gradeId,
                        Quantity = quantity
                    };
                    _userCartManager.AddToCart(cartLine);
                    return RedirectToAction("Index","Products");
            }

            }
            catch
            {

            }
            return RedirectToAction("Details", "Products", new { id = productId, supplierId = Request.Params["supplierId"] });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int? productId, string returnUrl)
        {
            var product = _productManager.RetrieveProducts()
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
                cart.RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails
            shippingDetails)
        {
            if (!cart.Lines.Any()) ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (!ModelState.IsValid) 
                return View(shippingDetails);

            if (_customerOrderManager.ProcessOrder(cart, shippingDetails)) //returns bool
            {
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View("Error");
            }
  
        }
    }
}