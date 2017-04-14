using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;
using DataObjects;

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
        /// Created: 2017/02/16
        /// 
        /// Attempts to create an Inspection record to the DB.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Changed signature from list of inspection fields to inspection itself.
        /// </remarks>
        /// <param name="inspection">The inspection to create in the database.</param>
        /// <returns>True if successful</returns>
        public bool CreateInspection(Inspection inspection)
        {
            bool added = false;

            try
            {
                added = (1 == InspectionAccessor.CreateInspection(inspection));
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
