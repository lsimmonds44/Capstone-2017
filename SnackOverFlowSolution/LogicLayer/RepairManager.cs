using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class RepairManager : IRepairManager
    {

        public List<Repair> RetreiveAllRepairsForVehicle(int vehicleId)
        {
            List<Repair> repairs = new List<Repair>();
            try
            {
                repairs = RepairAccessor.RetreiveAllRepairsForVehicle(vehicleId);
                foreach(Repair repair in repairs){
                    repair.RepairLineList = RepairLineAccessor.RetreiveAllRepairLinesForRepair(repair.RepairId);
                }
            }
            catch
            {
                throw;
            }

            return repairs;
        }
    }
}
