using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IUserCartManager
    {
        List<UserCartLine> RetrieveUserCart(String userID);
        int RemoveFromCart(int productId, string gradeId, int quantity, int userId);

        int AddToCart(UserCartLine toAdd);
    }
}
