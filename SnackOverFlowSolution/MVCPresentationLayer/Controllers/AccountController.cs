using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVCPresentationLayer.Models;
using LogicLayer;
using DataObjects;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Ariel Sigo 
    /// 
    /// Created:
    /// 2017/04/29
    /// The Controller for the Account
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserManager _appUserManager;
        private IUserCartManager _userCartManager;
        private IProductOrderManager _orderManager;

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// Create instances for IUserManager, IUserCartManager, and IProductManager
        /// </summary>
        /// <param name="appUserManager"></param>
        /// <param name="_userCartManager"></param>
        /// <param name="_orderManager"></param>
        public AccountController(IUserManager appUserManager, IUserCartManager _userCartManager, IProductOrderManager _orderManager)
        {
            this._appUserManager = appUserManager;
            this._userCartManager = _userCartManager;
            this._orderManager = _orderManager;
        }


        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// Creates instances for the Account Controller
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="appUserManager"></param>
        /// <param name="_userCartManager"></param>
        /// <param name="_orderManager"></param>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IUserManager appUserManager,
            IUserCartManager _userCartManager, IProductOrderManager _orderManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._appUserManager = appUserManager;
            this._userCartManager = _userCartManager;
            this._orderManager = _orderManager;
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// Instance of ApplicationSingInManager named SignInManager
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// 
        /// Instnace of ApplicationUserManager named UserManager
        /// 
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

     
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /Account/Login
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>View</returns>
        [AllowAnonymous]
        
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

   
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /Account/Login
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>View(model)</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Check if user exists
            var context = new ApplicationDbContext();
            var user = context.Users.FirstOrDefault(x => x.Email == model.UserName) ??
                       context.Users.FirstOrDefault(x => x.UserName == model.UserName);


            //If not approved, HasOrAssignRoles returns false
            if (!HasOrAssignRoles(context, model))//&& user != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                return View("ApprovalStatus");
            }
            
                

            //Finds username in case of email is provided during login
            if (user != null) model.UserName = user.UserName;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

      
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /Account/VerifyCode
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns>View</returns>
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

      
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// POST: /Account/VerifyCode
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View(model)</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /Account/Register 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /Account/Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View(Model)</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

           if (!ModelState.IsValid)
            {
                return View(model);
            }

            DataObjects.User userFound = null;

            try
            {
                userFound = _appUserManager.AuthenticateWebUser(model.Email, model.Password);
            }
            catch ( Exception ex )
            {
                Debug.WriteLine(ex.Message);
            }
            if (null != userFound)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var user = new ApplicationUser {UserName=userFound.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, userFound.FirstName));
                    //UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Surname, userFound.LastName));
                    //UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Email, userFound.EmailAddress));

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(model);
        }

     
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// 
        /// Created:
        /// 
        ///2017/04/29
        /// 
        /// GET: /Account/ConfirmEmail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns>View if succeeded</returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        
      
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /Account/ForgotPassword
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// POST: /Account/ForgotPassword 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View(model)</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

   
        /// <summary>
        /// Ariel Sigo  
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /Account/ForgotPasswordConfirmation
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// GET: /Account/ResetPassword 
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Error or View</returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// POST: /Account/ResetPassword 
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Error is fails View if successful</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        
    
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 
        /// 2017/04/29
        /// 
        /// GET: /Account/ResetPasswordConfirmation
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

       
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /Account/ExternalLogin 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns>New ChallengeResult </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /Account/SendCode 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns>View(new SendCodeViewModel) if successful</returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /Account/SendCode
        /// </summary>
        /// <param name="model"></param>
        /// <returns>View if successful or Error if unsuccessful</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

     
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /Account/ExternalLoginCallback
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>Redirect to Login if null, View if successful</returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

   
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /Account/ExternalLoginConfirmation
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>ActionResult Index if authenticated. Error if info is null</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /Account/LogOff
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        ///  GET: /Account/ExternalLoginFailure
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        /// <summary>
        /// William Flood
        /// 
        /// Created:
        /// 2017/04/06
        /// 
        /// 
        /// </summary>
        /// <returns>View(pageModel)</returns>
        public ActionResult ViewCart()
        {
            var userName = User.Identity.Name;
            var pageModel = new CartPageModel();

            // Access IClaimsIdentity which contains claims
            //IClaimsIdentity claimsIdentity = (IClaimsIdentity)icp.Identity;
            try
            {
                pageModel.ItemsInCart = _userCartManager.RetrieveUserCart(userName);
                pageModel.SavedOrderList = _orderManager.RetrieveSaveOrders(userName);
            }
            catch
            {
                return new HttpStatusCodeResult(500);
            }
            return View(pageModel);
        }

        /// <summary>
        /// William Flood
        /// Created: 
        /// 
        /// 2017/04/13
        /// </summary>
        /// <returns>ViewCart if successful, HttpStatusCodeResult if error</returns>
        public ActionResult RemoveFromCart()
        {
            try
            {
                var userId = Int32.Parse(Request.Params["userId"]);
                var productId = Int32.Parse(Request.Params["productId"]);
                var quantity = Int32.Parse(Request.Params["quantity"]);
                var gradeId = Request.Params["gradeId"];
                if (0 < _userCartManager.RemoveFromCart(productId, gradeId, quantity, userId))
                {
                    return RedirectToAction("ViewCart");
                }
                else
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(500);
            }
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Michael Takrama
        /// 
        /// Created:
        /// 4/22/2017
        /// 
        /// Assigns Roles to Users Based On Approval Status from Snack Overflow
        /// </summary>
        /// <remarks>
        /// Modified by Christian Lopez
        /// 2017/05/05
        /// 
        /// Fix introduced bug when logging in with unregistered user
        /// </remarks>
        /// <param name="context">DbContext</param>
        /// <param name="model">Login View Model</param>
        /// <returns>Boolean indicatinng Role Assignment Success</returns>
        public bool HasOrAssignRoles(ApplicationDbContext context, LoginViewModel model)
        {


            DataObjects.User usr = null;



            User userFound = null;
            try
            {
                userFound = _appUserManager.AuthenticateWebUser(model.UserName, model.Password); //uses only email
            }
            catch
            {

            }
            try
            {
                if (_appUserManager.AuthenticateUser(model.UserName, model.Password))
                {
                    usr = _appUserManager.RetrieveUserByUserName(model.UserName);
                }
                //usr = _appUserManager.RetrieveUserByUserName(userFound.UserName);
                
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }

            var user = context.Users.FirstOrDefault(x => x.Email == model.UserName) ??
                       context.Users.FirstOrDefault(x => x.UserName == model.UserName);

            if (user == null || usr == null)
            {
                model.Password = "";
                ModelState.AddModelError("Password", "Incorrect username or password");
                return false;
            }

            //Force email
            if (user != null && usr.EmailAddress != null)
            {
                model.UserName = usr.EmailAddress;

            }

            //if (null == userFound)
            //{
            //    model.Password = "";
            //    ModelState.AddModelError("Password", "Incorrect username or password");
            //}

            //var identityUserRoles = user.Roles;
            //if (identityUserRoles.Count != 0)
            //    return true;

            if (null != usr)
            {
                var identityUserRoles = user.Roles;
                if (identityUserRoles.Count != 0)
                    return true;
                
                bool[] roles = null;
                try
                {
                    roles = _appUserManager.GetUserRoles(usr.UserId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                //var store = new UserStore<ApplicationUser>(context);
                //var manager = new UserManager<ApplicationUser>(store);

                if (user == null)
                    return false;

                //var identityUserRoles = user.Roles;

                //if (identityUserRoles.Count != 0)
                //    return true;

                if (roles != null)
                {
                    if (roles[0])
                    {
                        UserManager.AddToRole(user.Id, "Customer");
                        return true;
                    }
                    if (roles[1])
                    {
                        UserManager.AddToRole(user.Id, "Employee");
                        return true;
                    }
                    if (roles[2])
                    {
                        UserManager.AddToRole(user.Id, "Supplier");
                        return true;
                    }
                }
            }

            return false;

        }
        /// <summary>
        /// William Flood
        /// Created:
        /// 2017/04/27
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>ViewCart</returns>
        public ActionResult LoadOrder(int? orderID)
        {
            try
            {
                _orderManager.LoadOrder((int)orderID);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("ViewCart");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}