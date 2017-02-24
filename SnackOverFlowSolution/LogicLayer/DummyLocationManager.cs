using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class DummyLocationManager : ILocationManager
    {
        int LocationIDToSend = 10000;
        public List<Location> ListLocations()
        {
            return new List<Location>(new Location[]{ new Location (){ LocationId = LocationIDToSend }});
        }


        public int CreateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public int DeactivateLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public int UpdateLocation(Location oldLocation, Location newLocation)
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
    }
}
