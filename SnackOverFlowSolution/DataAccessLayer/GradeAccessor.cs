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
    /// Christian Lopez
    /// Created: 2017/02/22
    /// 
    /// Class to handle database interactions involving grades.
    /// </summary>
    public class GradeAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/09
        /// 
        /// Gets a list of acceptable grades
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method. Changed return type from string array to list of string.
        /// </remarks>
        /// <returns>List of grades in the database</returns>
        public static List<string> RetrieveGradeList()
        {
            var grades = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_grade_list";
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
                        grades.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return grades;
        }
    }
}
