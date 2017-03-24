using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Created by Michael Takrama
    /// 24/03/2017
    /// 
    /// Logic for Preference Settings
    /// </summary>
    public class PreferenceManager : IPreferenceManager
    {
        /// <summary>
        /// Created by Michael Takrama
        /// 24/03/2017
        /// 
        /// Updates Preference Settings
        /// </summary>
        /// <param name="preferenceSetting"></param>
        /// <returns></returns>
        public bool UpdatePreferenceSettings(Preferences preferenceSetting)
        {
            return PreferenceAccessor.SavePreferenceSettings(preferenceSetting);
        }
    }
}
