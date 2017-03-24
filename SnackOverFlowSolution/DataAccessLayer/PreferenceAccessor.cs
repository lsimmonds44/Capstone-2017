using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PreferenceAccessor
    {
        /// <summary>
        /// Created by Michael Takrama
        /// 24/03/2017
        /// 
        /// Data Access Logic for Preference Settings
        /// </summary>
        /// <param name="preferenceSetting"></param>
        /// <returns>Returns a signal for success</returns>
        public static bool SavePreferenceSettings(DataObjects.Preferences preferenceSetting)
        {
            int count = 0;

            var conn = DBConnection.GetConnection();
            const string cmdText = @"sp_update_preferenceSettings";
            var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.AddWithValue("@expiringsoonduration", preferenceSetting.expiringSoonDuration);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

            return count > 0;
        }
    }
}
