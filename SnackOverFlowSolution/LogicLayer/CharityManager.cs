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
    public class CharityManager : ICharityManager
    {
        public List<Charity> RetrieveCharityList()
        {
            var accessor = CharityAccessor.GetCharityAccessorInstance();
            try
            {
                DatabaseMainAccessor.RetrieveList(accessor);
                return accessor.CharityList;
            } catch
            {
                throw;
            }
        }

        public int AddCharity(Charity charityInstance)
        {
            var accessor = CharityAccessor.GetCharityAccessorInstance();
            accessor.CharityInstance = charityInstance;
            try
            {
                return DatabaseMainAccessor.Create(accessor);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Daniel Brown
        /// Created 03/04/2017
        /// 
        /// Approves a charity application
        /// </summary>
        /// <param name="charityInstance"></param>
        /// <returns></returns>
        public bool ApproveCharity(Charity charityInstance)
        {
            bool result = false;
            CharityAccessor accessor = new CharityAccessor();

            try
            {
                if (accessor.ApproveCharity(charityInstance) > 0)
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
        /// Created 03/04/2017
        /// 
        /// Denies a charity application
        /// </summary>
        /// <param name="charityInstance"></param>
        /// <returns></returns>
        public bool DenyCharity(Charity charityInstance)
        {
            bool result = false;
            CharityAccessor accessor = new CharityAccessor();

            try
            {
                if (accessor.DenyCharity(charityInstance) > 0)
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
        /// Created 2017/03/08
        /// 
        /// Handles the logic of creating an application for a charity.
        /// </summary>
        /// <param name="charityInstance"></param>
        /// <returns></returns>
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
        /// 3/24/2017
        /// 
        /// Retrieves Charity instance by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Charity RetrieveCharityByUserId(int userId)
        {
            Charity s = null;

            try
            {
                s = CharityAccessor.RetrieveCharityByUserId(userId);
            }
            catch (Exception)
            {

                throw;
            }

            if (null == s)
            {
                throw new ApplicationException("Could not find supplier for that user ID.");
            }

            return s;
        }
    }
}
