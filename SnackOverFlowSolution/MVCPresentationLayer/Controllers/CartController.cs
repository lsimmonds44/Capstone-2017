﻿using System;
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
    /// Michael Takrama
    /// 
    /// Created:
    /// 2017/04/07
    /// 
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
        /// <returns>RedirectToActiion</returns>
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

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns>RedirectToAction</returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart, int? productId, string returnUrl)
        {
            var product = _productManager.RetrieveProducts()
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
                cart.RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>PartialView(cart)</returns>
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <returns>View(ShippingDetails)</returns>
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

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="shippingDetails"></param>
        /// <returns>View(Completed) if successful, else Error</returns>
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (!cart.Lines.Any()) 
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (!ModelState.IsValid)
                return View(shippingDetails);

            if (_customerOrderManager.ProcessOrder(cart, shippingDetails))
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