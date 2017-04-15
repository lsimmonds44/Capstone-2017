using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private IUserManager _userManager;
        private ICustomerManager _customerManager;

        public HomeController(IUserManager um, ICustomerManager cm)
        {
            _userManager = um;
            _customerManager = cm;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult RegisterCommercial()
        {
            return View();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 04/15/2017
        /// 
        /// Action to Register for Commercial Account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCommercial([Bind(Include = "FirstName, LastName, Phone, AddressLineOne, AddressLineTwo, City, State, Zip, EmailAddress, Username, Password, ConfirmPassword, FederalTaxID")] RegisterCommercialViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                return View(_customerManager.ApplyForCommercialAccount(user) ? "Application-Success" : "Error");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new ApplicationException(ex.Message);
            }

        }
        
    }
}