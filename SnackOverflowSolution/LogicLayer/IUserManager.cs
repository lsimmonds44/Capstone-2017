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
    }
}
