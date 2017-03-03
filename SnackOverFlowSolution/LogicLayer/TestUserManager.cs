using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class TestUserManager : IUserManager
    {
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/01
        /// 
        /// Return known values as a User
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Known User</returns>
        public DataObjects.User RetrieveUserByUserName(string userName)
        {
            User newUser = null;

            if (userName.Equals("testUserName"))
            {
                newUser = new User()
                {
                    UserId = 10001,
                    FirstName = "Test",
                    LastName = "User",
                    Phone = "1234567890",
                    PreferredAddressId = 1,
                    EmailAddress = "test@test.com",
                    EmailPreferences = true,
                    UserName = userName,
                    Active = true
                };
            }
            else
            {
                throw new ApplicationException("Unable to find user " + userName);
            }

            return newUser;
        }

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/01
        /// 
        /// Return known values as UserAddress
        /// </summary>
        /// <param name="prefferedAddressId">The preferred addressID from the User</param>
        /// <returns>Known User Address</returns>
        public DataObjects.UserAddress RetrieveUserAddress(int? prefferedAddressId)
        {
            UserAddress ua = null;

            if (prefferedAddressId == 1)
            {
                ua = new UserAddress()
                {
                    UserAddressId = (int)prefferedAddressId,
                    UserId = 10000,
                    AddressLineOne = "102 Somewhere Rd",
                    AddressLineTwo = "",
                    City = "Anywhere",
                    State = "IA",
                    Zip = "52525"
                };
            }
            else
            {
                throw new ApplicationException("Unable to find address");
            }

            return ua;
        }

        public string LogIn(string uName, string pass)
        {
            string result = "UserNotFound";
            if (uName == "testUserName" && pass == "password")
            {
                result = "Found";
            }
            return result;
        }

        public User userInstance
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool AuthenticateUser(string text, string password)
        {
            throw new NotImplementedException();
        }

        public List<User> RetrieveFullUserList()
        {
            throw new NotImplementedException();
        }

        public int ChangePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            throw new NotImplementedException();
        }

        public List<string> roles
        {
            get { throw new NotImplementedException(); }
        }


        public string NewPassword()
        {
            throw new NotImplementedException();
        }

        public int ResetPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
