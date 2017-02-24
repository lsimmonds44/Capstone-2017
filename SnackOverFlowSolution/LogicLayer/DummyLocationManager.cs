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
            return new List<Location>(new Location[]{ new Location (){ LocationID = LocationIDToSend }});
        }
    }
}
