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

        /// <summary>
        /// Created by Bill Flood
        /// Edited by Michael Takrama on 3/2/2017
        /// 
        /// Retrieves locatio by id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Location RetrieveLocationByID(int locationId)
        {
            var location = LocationAccessor.RetrieveLocationById(locationId);

            return location;
        }

        public int UpdateLocation(Location oldLocation, Location newLocation)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/03
        /// 
        /// Get a list of locations from the location accessor
        /// </summary>
        /// <returns></returns>
        public List<Location> ListLocations()
        {
            List<Location> locations = null;
            try
            {
                locations = LocationAccessor.RetrieveAllLocations();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return locations;
        }
    }
}
