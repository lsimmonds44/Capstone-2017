using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Author: Skyler Hiscock
        /// Updated: 4/28/17
        /// </summary>
        /// <param name="username"></param>
        /// <param name="supOrCom">true = "Supplier"; false = "Commercial"</param>
        /// <returns></returns>
        
        public ActionResult ApplicationSuccess(string username, bool supOrCom)
        {
            string supplierOrCommercial = supOrCom ? "Supplier" : "Commercial"; 
            var _user = _userManager.RetrieveUserByUserName(username);
            var successViewModel = new HomeViewModels.ApplicationSuccessViewModel()
            {
                SupplierOrCommercial = supplierOrCommercial,
                FirstName = _user.FirstName,
                LastName = _user.LastName
            };
            return View(successViewModel);
        }

        public ActionResult RegisterCommercial()
        {
            return View();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 04/15/2017
        /// Updated by Skyler Hiscock
        /// 04/28/17
        /// 
        /// Action to Register for Commercial Account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCommercial([Bind(Include = "FirstName, LastName, Phone, AddressLineOne, AddressLineTwo, City, State, Zip, EmailAddress, Username, Password, ConfirmPassword, FederalTaxID")] RegisterCommercialViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                // SnackOverflow System Application
                if (_customerManager.ApplyForCommercialAccount(user))
                {
                    var rvm = new RegisterViewModel { Email = user.EmailAddress, Password = user.Password };

                    var controller = DependencyResolver.Current.GetService<AccountController>();

                    controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);

                    var result = await controller.Register(rvm);


                    ViewBag.SupplierOrCommercial = "Commercial";
                    return RedirectToAction("ApplicationSuccess", new { username = user.UserName, supOrCom = false });
                }
                else
                {
                    return View("Error");
                }
            }
            catch (ApplicationException ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new ApplicationException(ex.Message);
            }

        }

    }
}