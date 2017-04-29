namespace MVCPresentationLayer.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentationLayer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentationLayer.Models.ApplicationDbContext>
    {
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/04/14
        /// 
        /// Adding Admin and other roles - Mimic code used in .NET class
        /// </summary>
        /// <remarks>William Flood enabled Migrations 2017/04/13</remarks>
        /// <param name="context"></param>
        protected override void Seed(MVCPresentationLayer.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // check for an admin, and if not there, create one
            if (!context.Users.Any(u => u.UserName == "admin@test.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                    //FirstName = "System",
                    //LastName = "Admin"
                };

                IdentityResult result = userManager.Create(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    // add claims if we want - don't do first one
                    //userManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "Administrator"));
                    userManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, "System"));
                    userManager.AddClaim(user.Id, new Claim(ClaimTypes.Surname, "Administrator"));
                }

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Administrator" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Administrator");
                context.SaveChanges();
            }

            // Add the roles based on tables in the existing internal system
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Employee" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Customer" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Supplier" });

            context.SaveChanges();

        }
    }
}
