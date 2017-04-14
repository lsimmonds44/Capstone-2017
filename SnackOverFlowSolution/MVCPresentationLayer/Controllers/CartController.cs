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
            var product = _productManager.RetrieveProducts()
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
                cart.AddItem(product, 1);
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

            _customerOrderManager.ProcessOrder(cart, shippingDetails); //returns bool




            cart.Clear();
            return View("Completed");
        }
    }
}