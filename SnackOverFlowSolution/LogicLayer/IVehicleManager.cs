using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IVehicleManager
    {
        Vehicle RetreiveVehicleById(int vehicleId);
        /// <summary>
        /// Eric Walton
        /// 2017/02/03
        /// 
        /// </summary>
        /// <returns></returns>
        List<Vehicle> RetrieveAllVehicles();
        int CreateVehicle(Vehicle newVehicle);
    }
}
