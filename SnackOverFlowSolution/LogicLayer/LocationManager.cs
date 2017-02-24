using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class LocationManager : ILocationManager
    {
        /// <summary>
        /// Author: Skyler Hiscock
        /// Creates warehouse location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>1 if successful, 0 if it fails</returns>
        public int CreateLocation(Location location)
        {
            try
            {
                return LocationAccessor.CreateLocation(location);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeactivateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public List<Location> RetrieveAllLocations(Location location)
        {
            throw new NotImplementedException();
        }

        public Location RetrieveLocationByID(int locationId)
        {
            throw new NotImplementedException();
        }

        public int UpdateLocation(Location oldLocation, Location newLocation)
        {
            throw new NotImplementedException();
        }
    }
}
