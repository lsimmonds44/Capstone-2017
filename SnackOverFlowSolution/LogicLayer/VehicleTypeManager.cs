using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class VehicleTypeManager : IVehicleTypeManager
    {
        /// <summary>
        /// Mason Allen
        /// Created 03/01/2017
        /// Returns a list of current vehicle types
        /// </summary>
        /// <returns>List<VehicleType></returns>
        public List<VehicleType> retreiveVehicleTypeList()
        {
            var vehicleTypeList = new List<VehicleType>();
            try
            {
                vehicleTypeList = VehicleTypeAccessor.RetreiveVehicleTypeList();
            }
            catch (Exception)
            {
                throw new ApplicationException("There was a problem retrieving the list of vehicle types");
            }
            return vehicleTypeList;
        }
    }
}
