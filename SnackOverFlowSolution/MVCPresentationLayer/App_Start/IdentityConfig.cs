using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer
{
    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Sends an email
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Sends an Email
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Task.FromResult</returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// Sends a text message
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        /// <summary>
        /// Ariel Sigo
        /// Udpated:
        /// 2017/04/29
        /// Sends a text message
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Task.FromResult</returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    
    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application. 
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Application user manager used in this application 
        /// </summary>
        /// <param name="store"></param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        /// <summary>
        /// Ariel Sigo
        /// Updated: 
        /// 2017/04/29
        /// Creates new options for ApplicationUser
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns>Manager if successful</returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 7,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

 
   /// <summary>
   /// Ariel Sigo
   /// Updated:
   /// 2017/04/29
    /// Configure the application sign-in manager which is used in this application.
   /// </summary>
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Validates the signIn Manager for Application User Manager
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Creates new userIdenity Async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        /// <summary>
        /// Ariel Sigo
        /// UPdated:
        /// 2017/04/29
        /// Creates IdentityFactoryOptions for ApplicationSignInManager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
