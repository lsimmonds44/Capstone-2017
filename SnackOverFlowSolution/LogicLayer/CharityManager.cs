using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using System.Data.SqlClient;

namespace LogicLayer
{
    /// <summary>
    /// Ariel Sigo 
    /// Updated:
    /// 2017/04/29
    /// 
    /// Charity Manager Class
    /// </summary>
    public class CharityManager : ICharityManager
    {
        /// <summary>
        /// Ariel Sigo 
        /// Updated:
        /// 2017/04/29
        ///
        /// </summary>
        /// <returns>List of Charities</returns>
        public List<Charity> RetrieveCharityList()
        {
            List<Charity> charities = new List<Charity>();
                           
            try
            {
                charities = CharityAccessor.RetrieveCharities();
            } 
            catch
            {
                throw;
            }

            return charities;
        }

        /// <summary>
        /// Ariel Sigo
        /// Upated:
        /// 2017/04/29
        /// </summary>
        /// <param name="charity"></param>
        /// <returns>int of rows affected if succesful</returns>
        public int AddCharity(Charity charity)
        {
            try
            {
                return CharityAccessor.CreateCharityApplication(charity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Daniel Brown
        /// Created 
        /// 2017/03/04
        /// 
        /// Approves a charity application
        /// </summary>
        /// <param name="charityInstance"></param>
        /// <returns>True if successful</returns>
        public bool ApproveCharity(Charity charityInstance)
        {
            bool result = false;

            try
            {
                if (CharityAccessor.ApproveCharity(charityInstance) > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }

        /// <summary>
        /// Daniel Brown
        /// Created 
        /// 2017/03/04
        /// 
        /// Denies a charity application
        /// </summary>
        /// <param name="charityInstance"></param>
        /// <returns>true if successful</returns>
        public bool DenyCharity(Charity charityInstance)
        {
            bool result = false;

            try
            {
                if (CharityAccessor.DenyCharity(charityInstance) > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 
        /// 2017/03/08
        /// 
        /// Handles the logic of creating an application for a charity.
        /// </summary>
        /// <param name="charityInstance"></param>
        /// <returns>true if successful</returns>
        public bool AddCharityApplication(Charity charityInstance)
        {
            bool result = false;

            try
            {
                if (CharityAccessor.CreateCharityApplication(charityInstance) == 1)
                {
                    result = true;
                }
            }
            catch (SqlException ex)
            {
                
                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
            
            return result;
        }

        /// <summary>
        /// Bobby Thorne
        /// Created:
        /// 2017/03/24
        /// 
        /// Retrieves Charity instance by user id
        /// </summary>
        /// <remarks>
        /// Christian Lopez
        /// 2017/05/07
        /// 
        /// Removed due to business rules separating user from charity
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>Charity if successful</returns>
        //public Charity RetrieveCharityByUserId(int userId)
        //{
        //    Charity s = null;

        //    try
        //    {
        //        s = CharityAccessor.RetrieveCharityByUserId(userId);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    if (null == s)
        //    {
        //        throw new ApplicationException("Could not find supplier for that user ID.");
        //    }

        //    return s;
        //}
    }
}
