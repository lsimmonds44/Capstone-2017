using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/16
    /// 
    /// Handles the logic regarding Inspections
    /// </summary>
    public class InspectionManager : IInspectionManager
    {
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/16
        /// 
        /// Attempts to create an Inspection record to the DB.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="productLotId"></param>
        /// <param name="gradeId"></param>
        /// <param name="datePerformed"></param>
        /// <param name="expirationDate"></param>
        /// <returns>True if successful</returns>
        public bool CreateInspection(int employeeID, int productLotId, string gradeId,
            DateTime datePerformed, DateTime expirationDate)
        {
            bool added = false;

            try
            {
                added = (1 == InspectionAccessor.CreateInspection(employeeID, productLotId, gradeId, datePerformed, expirationDate));
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return added;
        }
    }
}
