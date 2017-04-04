using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DatabaseMainAccessor
    {
        /// <summary>
        /// Adds a new record to the database
        /// Added February 10, 2017 by William Flood
        /// </summary>
        /// <param name="accessor">An IDataAccessor specific to the entity used</param>
        /// <returns></returns>
        public static int Create(IDataAccessor accessor)
        {
            var results = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand(accessor.CreateScript, conn);
            accessor.SetCreateParameters(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                results = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); // good housekeeping approved!
            }
            return results;
        }

        /// <summary>
        /// Retrieves all records from a given table
        /// Added February 10, 2017 by William Flood
        /// </summary>
        /// <param name="accessor">An IDataAccessor specific to the entity used</param>

        public static void RetrieveList(IRetriever accessor)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand(accessor.RetrieveListScript, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                accessor.ReadList(reader);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); // good housekeeping approved!
            }
        }
    }
}
