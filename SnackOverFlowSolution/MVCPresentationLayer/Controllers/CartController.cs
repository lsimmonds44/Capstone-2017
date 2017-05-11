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
    /// Michael Takrama
    /// 
    /// Created:
    /// 2017/04/07
    /// 
    /// 
    /// Cart Controller
    /// </summary>
    /// <remarks>
    /// Modified by Christian Lopez
    /// 2017/05/05
    /// 
    /// Made class authorized to customer - this fits business rules as well as
    /// fixes bugs looking when trying to find a non-existant user.
    /// </remarks>
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICustomerOrderManager _customerOrderManager;
        private readonly IUserManager _userManager;
        private readonly IUserCartManager _userCartManager;
        private readonly ICommercialInvoiceManager _invoiceManager;

        public CartController(IProductManager repo, IUserManager _userManager, IUserCartManager _userCartManager, ICommercialInvoiceManager _invoiceManager)
        {
            _productManager = repo;
            this._userCartManager = _userCartManager;
            this._userManager = _userManager;
            this._invoiceManager = _invoiceManager;
        }

        public CartController(IProductManager repo, ICustomerOrderManager proc, IUserManager _userManager, IUserCartManager _userCartManager, ICommercialInvoiceManager _invoiceManager)
        {
            _productManager = repo;
            this._customerOrderManager = proc;
            this._userCartManager = _userCartManager;
            this._userManager = _userManager;
            this._invoiceManager = _invoiceManager;
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
                    var results = _userCartManager.AddToCart(cartLine);
                    if (results >= 0)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        return RedirectToAction("Details", "Products", new { id = productId, supplierId = Request.Params["supplierId"], errored="runout" });
                    }
                    
                }

            }
            catch (Exception)
            {
            
                //var test = "breaker";
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

            if (!ModelState.IsValid)
            { 
                return View(shippingDetails);
            }

            int orderID = -7;

            try
            {
                orderID = _customerOrderManager.ProcessOrder(shippingDetails);
                if (orderID==0)
                {
                    ViewBag.Error = "Your cart is empty.";
                    return View();
                }
                else
                {
                    decimal taxRate = Decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["TaxRate"]);
                    _invoiceManager.CreateCustomerInvoice(orderID, taxRate);
                    return View("Completed");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occured: " + ex.Message+(null==ex.InnerException?"":ex.InnerException.Message);
                return View();
            }

        }
    }
}