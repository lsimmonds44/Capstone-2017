using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class CharityManager : ICharityManager
    {
        public List<Charity> RetrieveCharityList()
        {
            var accessor = new CharityAccessor();
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
            var accessor = new CharityAccessor();
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
            catch (Exception)
            {
                
                throw;
            }
            
            return result;
        }
    }
}
