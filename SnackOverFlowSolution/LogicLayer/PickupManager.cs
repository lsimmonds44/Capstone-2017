using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Robert Forbes
    /// 2017/04/13
    /// </summary>
    public class PickupManager : IPickupManager
    {
        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public List<Pickup> RetrievePickupsForDriver(int? driverId)
        {
            List<Pickup> pickups = new List<Pickup>();
            try
            {
                pickups = PickupAccessor.RetrievePickupsForDriver(driverId);
                foreach(Pickup p in pickups){
                    p.PickupLineList = PickupLineAccessor.RetrievePickupLinesForPickup(p.PickupId);
                    foreach(PickupLine line in p.PickupLineList){
                        line.productName = ProductAccessor.RetrieveProduct((int)line.ProductId).Name;
                    }
                    p.address = SupplierAccessor.RetrieveUserAddressBySupplier(p.SupplierId);
                }
            }
            catch
            {
                throw;
            }

            return pickups;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// 
        /// Returns all pickups for a driver that haven't already been picked up
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public List<Pickup> RetrieveUnpickedupPickupsForDriver(int? driverId)
        {
            List<Pickup> pickups = new List<Pickup>();
            try
            {
                pickups = RetrievePickupsForDriver(driverId);

                List<Pickup> pickupsToRemove = new List<Pickup>();
                foreach(Pickup p in pickups){
                    List<PickupLine> linesToRemove = new List<PickupLine>();

                    foreach(PickupLine line in p.PickupLineList){
                        if(line.PickupStatus == true){
                            linesToRemove.Add(line);
                        }
                    }

                    foreach(PickupLine line in linesToRemove){
                        p.PickupLineList.Remove(line);
                    }


                    if(!p.PickupLineList.Any()){
                        pickupsToRemove.Add(p);
                    }
                }

                foreach (Pickup p in pickupsToRemove)
                {
                    pickups.Remove(p);
                }
            }
            catch
            {
                throw;
            }

            return pickups;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/19
        /// </summary>
        /// <param name="oldLine"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        public bool UpdatePickupLine(PickupLine oldLine, PickupLine newLine)
        {
            bool result = false;

            try
            {
                result = (PickupLineAccessor.UpdatePickupLine(oldLine, newLine) >= 1);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/19
        /// </summary>
        /// <param name="pickupLineId"></param>
        /// <returns></returns>
        public PickupLine RetrievePickupLineById(int? pickupLineId)
        {
            PickupLine result = null;
            try
            {
                result = PickupLineAccessor.RetrievePickupLine(pickupLineId);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/27/2017
        /// 
        /// Retrieves a list of pickup lines, adding only those picked up
        /// </summary>
        /// <returns></returns>
        public List<PickupLineAndProductName> RetrievePickupLinesReceived()
        {
            List<PickupLineAndProductName> pickupList = new List<PickupLineAndProductName>();
            bool pickupStatus = true;

            try
            {
                pickupList = PickupLineAccessor.RetrievePickupLinesReceived(pickupStatus);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return pickupList;
        }


        public Pickup RetrievePickupById(int? pickupId)
        {
            Pickup pickup = null;

            try
            {
                pickup = PickupAccessor.RetrievePickupById(pickupId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return pickup;
        }

        public bool DeletePickupLine(PickupLine pickupLine)
        {
            bool result = false;

            try
            {
                result = (PickupLineAccessor.DeletePickupLine(pickupLine) >= 1);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/30
        /// </summary>
        /// <param name="pickupId"></param>
        /// <returns></returns>
        public List<PickupLine> RetrievePickupLinesByPickupId(int? pickupId)
        {
            List<PickupLine> result = new List<PickupLine>();

            try
            {
                result = PickupLineAccessor.RetrievePickupLinesForPickup(pickupId);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Laura Simmonds
        /// Created: 2017/05/10
        /// </summary>
        /// <param name="pickup"></param>
        /// <returns></returns>
        public int CreatePickup(Pickup pickup)
        {
            int pickupID = 0;
            try
            {
                pickupID = PickupAccessor.CreatePickup(pickup);
            }
            catch (Exception)
            {
                throw;
            }
            return pickupID;
        }

    }
}
