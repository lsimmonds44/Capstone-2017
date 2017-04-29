using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCPresentationLayer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created;
    /// 2017/04/29
    /// 
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <param name="manager"></param>
        /// <returns>user Identity</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <returns>New Application Database context</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}