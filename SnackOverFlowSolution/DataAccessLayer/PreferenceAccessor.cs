using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Aaron Usher
    /// Updated: 
    /// 2017/04/21
    /// 
    /// Class to handle database interactions involving preferences.
    /// </summary>
    public static class PreferenceAccessor
    {
        /// <summary>
        /// Michael Takrama
        /// Created: 
        /// 2017/03/24
        /// 
        /// Data Access Logic for Preference Settings.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Update: 
        /// 2017/04/24
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="preferenceSetting">The setting to update.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdatePreferenceSettings(Preferences preferenceSetting)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_preferenceSettings";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@expiringsoonduration", preferenceSetting.ExpiringSoonDuration);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
