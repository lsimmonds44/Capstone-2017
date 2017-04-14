using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserManager
    {
        User userInstance { get; set; }
        User RetrieveUserByUserName(string userName);
        bool AuthenticateUser(string text, string password);
        List<User> RetrieveFullUserList();
        List<String> roles { get; }


        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/01
        /// 
        /// Get a UserAddress from the perferred address ID
        /// </summary>
        /// <param name="prefferedAddressId">The ID from the User</param>
        /// <returns></returns>
        UserAddress RetrieveUserAddress(int? prefferedAddressId);

        string LogIn(string p1, string p2);
        string RetrieveUsernameByEmail(string email);
        int ChangePassword(String userName, String oldPassword, String newPassword, String confirmPassword);
        String NewPassword();
        int ResetPassword(String userName, String password);

        User RetrieveUser(int userId);
        User AuthenticateWebUser(String email, String password);
    }
}
