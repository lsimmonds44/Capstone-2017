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




        string LogIn(string p1, string p2);
        string RetrieveUsernameByEmail(string email);
        int ChangePassword(String userName, String oldPassword, String newPassword, String confirmPassword);
        string NewPassword();
        int ResetPassword(String userName, String password);

        User RetrieveUser(int userId);
        User AuthenticateWebUser(String email, String password);
    }
}
