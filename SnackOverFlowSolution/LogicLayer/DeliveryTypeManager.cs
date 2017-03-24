using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Aaron Usher
    /// Created: 2017/03/24
    /// 
    /// Class to reprsent a delivery type manager.
    /// </summary>
    public class DeliveryTypeManager : IDeliveryTypeManager
    {
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/03/24
        /// 
        /// Retrieves a list of all delivery types.
        /// </summary>
        /// <returns>A list of delivery types.</returns>
        public List<string> RetrieveDeliveryTypeList()
        {
            List<string> result = null;
            try
            {
                result = DeliveryTypeAccessor.RetrieveDeliveryTypeList();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
