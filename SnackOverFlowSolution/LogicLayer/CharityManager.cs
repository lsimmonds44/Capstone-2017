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
    }
}
