using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class DriverManager : IDriverManager
    {
        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        /// <returns></returns>
        public List<Driver> RetrieveAllDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            try
            {
                drivers = DriverAccessor.RetrieveAllDrivers();
            }
            catch
            {
                throw new ApplicationException("Failed To Retrieve Drivers");
            }
            return drivers;
        }
    }
}
