using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using MVCPresentationLayer.Models;
using Microsoft.AspNet.Identity;

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
            this._customerOrderManager = proc;
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
        [Authorize(Roles = "Supplier, Customer")]
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
                    
                    var currentUser = _userManager.RetrieveUserByUserName(User.Identity.Name);
                    var gradeId = Request.Params["Grade"];

                    var quantity = Int32.Parse(Request.Params["Quantity"]);
                    cart.AddItem(product, quantity);
                    var cartLine = new UserCartLine
                    {
                        UserID = currentUser.UserId,
                        ProductID = (int)productId,
                        GradeID = gradeId,
                        Quantity = quantity
                    };
                    _userCartManager.AddToCart(cartLine);
                    return RedirectToAction("Index", "Products");
                }

            }
            catch
            {

            }
            return RedirectToAction("Details", "Products", new { id = productId, supplierId = Request.Params["supplierId"] });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            var identityUserName = User.Identity.GetUserName();
            User user = _userManager.RetrieveUserByUserName(identityUserName);
            var shippingDetails = new ShippingDetails()
            {
                Line1 = user.AddressLineOne == null ? "" : user.AddressLineOne,
                Line2 = user.AddressLineTwo == null ? "" : user.AddressLineTwo,
                City = user.City == null ? "" : user.City,
                State = user.State == null ? "" : user.State,
                Zip = user.Zip == null ? "" : user.Zip,
                CustomerId = user.UserId,
                IdentityUsername = identityUserName
            };

            return View(shippingDetails);
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {

            if (!ModelState.IsValid)
            { 
                return View(shippingDetails);
            }

            int orderID = -7;

            try
            {
                orderID = _customerOrderManager.ProcessOrder(shippingDetails);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occured: " + ex.Message;
            }
            if (orderID==0)
            {
                ViewBag.Error = "Your cart is empty.";
                return View();
            }
            else
            {
                return View("Completed");
            }

        }
    }
}