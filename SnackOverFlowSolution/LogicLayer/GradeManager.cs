using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class GradeManager : IGradeManager
    {
        /// <summary>
        /// Christian Lopez
        /// 2017/02/22
        /// 
        /// Returns a list of grades for a combo box
        /// </summary>
        /// <returns></returns>
        public List<string> RetrieveGradeList()
        {
            List<string> grades = null;
            try
            {
                grades = GradeAccessor.RetrieveGradeList();
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return grades;
        }
    }
}
