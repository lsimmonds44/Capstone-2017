using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class TestManager
    {

        public bool DeleteTestUser()
        {
            bool result = false;

            try
            {
                if (1 == TestAccessor.DeleteTestUser())
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

        public bool DeleteTestEmployee()
        {
            bool result = false;

            try
            {
                if (1 == TestAccessor.DeleteTestEmployee())
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

        public bool DeleteTestProduct()
        {
            bool result = false;

            try
            {
                if (1 == TestAccessor.DeleteTestProduct())
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
