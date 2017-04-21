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
    /// Created: 2017/04/21
    /// 
    /// Class to handle database interactions involving statuses.
    /// </summary>
    public static class StatusAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Retrieves a list of all statuses from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>A list of all statuses from the database.</returns>
        public static List<string> RetrieveStatusList()
        {
            var statuses = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_status_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        statuses.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return statuses;
        }
    }
}
