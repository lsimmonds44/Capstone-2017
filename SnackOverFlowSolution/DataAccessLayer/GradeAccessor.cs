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
    /// 2017/02/22
    /// </summary>
    public class GradeAccessor
    {
        /// <summary>
        /// Created by Christian Lopez
        /// Created on 2017/02/09
        /// 
        /// Gets a list of acceptable grades
        /// </summary>
        /// <returns>List of grades in the database</returns>
        public static string[] RetrieveGradeList()
        {
            string[] grades = null;

            List<string> dataGrades = new List<string>();

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
                        dataGrades.Add(reader.GetString(0));
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

            grades = dataGrades.Select(g => g.ToString()).ToArray();

            return grades;
        }
    }
}
