using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserCartManager : IUserCartManager
    {

        public List<UserCartLine> RetrieveUserCart(int userID)
        {
            List<UserCartLine> userCart = null;
            try
            {
                userCart = UserCartAccessor.RetrieveCartForUser(userID);
            } catch (Exception ex) {
                throw new ApplicationException("Error retrieving information:",ex);
            }
            return userCart;
        }
    }
}
