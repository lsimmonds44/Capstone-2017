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
    /// Class to represent a status manager.
    /// </summary>
    public class StatusManager : IStatusManager
    {
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/03/24
        /// 
        /// Retrieves a list of statuses.
        /// </summary>
        /// <returns>A list of statuses.</returns>
        public List<string> RetrieveStatusList()
        {
            List<string> result = null;
            try
            {
                result = StatusAccessor.RetrieveStatusList();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
