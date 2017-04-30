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
    /// <summary>
    /// Ariel Sigo
    /// 
    /// 2017/04/29
    /// 
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        private IUserManager _userManager;
        private ICustomerManager _customerManager;

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="um"></param>
        /// <param name="cm"></param>
        public HomeController(IUserManager um, ICustomerManager cm)
        {
            _userManager = um;
            _customerManager = cm;
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <returns>View of Index</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <returns>View of About</returns>
        public ActionResult About()
        {
            return View();
        }


        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <returns>View of Contact</returns>
        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Skyler Hiscock
        /// 
        /// Created:
        /// 2017/04/21
        /// 
        /// Updated:
        /// 2017/04/28
        /// </summary>
        /// <param name="username"></param>
        /// <param name="supOrCom">true = "Supplier"; false = "Commercial"</param>
        /// <returns>View of successViewModel</returns>
        
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

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <returns>View of RegisterCommercial</returns>
        public ActionResult RegisterCommercial()
        {
            return View();
        }

        /// <summary>
        /// Michael Takrama
        /// 
        /// Created:
        /// 2017/04/15
        /// 
        /// Skyler Hiscock
        /// Updated:
        /// 2017/04/28
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