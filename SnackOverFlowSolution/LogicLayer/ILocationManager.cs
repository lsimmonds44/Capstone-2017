using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ILocationManager
    {
        List<Location> ListLocations();
		
        int CreateLocation(Location location);

        int DeactivateLocation(Location location);

        int UpdateLocation(Location oldLocation, Location newLocation);

        List<Location> RetrieveAllLocations(Location location);

        Location RetrieveLocationByID(int locationId);

    }
}
