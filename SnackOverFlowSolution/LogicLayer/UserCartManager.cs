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

        public List<UserCartLine> RetrieveUserCart(String userName)
        {
            List<UserCartLine> userCart = null;
            try
            {
                userCart = UserCartAccessor.RetrieveCartForUser(userName);
            } catch (Exception ex) {
                throw new ApplicationException("Error retrieving information:",ex);
            }
            return userCart;
        }


        public int RemoveFromCart(int productId, string gradeId, int quantity, int userId)
        {
            try
            {
                return UserCartAccessor.RemoveFromCart(productId, gradeId, quantity, userId);
            } catch {
                throw;
            }
        }
    }
}
