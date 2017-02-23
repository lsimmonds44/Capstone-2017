using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class GradeManager : IGradeManager
    {

        public string[] RetrieveGradeList()
        {
            string[] grades = null;
            try
            {
                grades = GradeAccessor.RetrieveGradeList();
            }
            catch (Exception)
            {

                throw;
            }

            return grades;
        }
    }
}
