using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCPresentationLayer.Controllers.Api
{
    public class UserController : ApiController
    {
        IUserManager _userManager = new UserManager();

        /// <summary>
        /// Robert Forbes
        /// 2017/04/07
        /// 
        /// Api call to try retrieve a user with the entered user name and password
        /// Returns null if no user was found with the passed in login details
        /// </summary>
        /// <param name="userName">The username to search for</param>
        /// <param name="password">The password to search for</param>
        /// <returns>A user matching the passed in username and password or null</returns>
        [System.Web.Http.HttpGet]
        public User Login(string userName, string password)
        {
            try
            {
                if (_userManager.AuthenticateUser(userName, password))
                {
                    return _userManager.RetrieveUserByUserName(userName);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
