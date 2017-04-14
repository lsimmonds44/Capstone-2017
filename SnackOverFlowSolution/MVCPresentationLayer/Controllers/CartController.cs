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

        public CartController(IProductManager repo)
        {
            _productManager = repo;
        }

        public CartController(IProductManager repo, ICustomerOrderManager proc)
        {
            _productManager = repo;
            _customerOrderManager = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int? productId, string returnUrl)
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
            }
            var gradeId = Request.Params["Grade"];
            var quantity = Request.Params["Quantity"];

            return RedirectToAction("Index", new { returnUrl });
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