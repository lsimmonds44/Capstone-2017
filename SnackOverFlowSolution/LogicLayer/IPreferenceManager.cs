using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Created by Michael Takrama
    /// 03/24/2017
    /// 
    /// Interface for PreferenceManager
    /// </summary>
    public interface IPreferenceManager
    {
        bool UpdatePreferenceSettings(Preferences preferenceSetting);
    }
}
