using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/10
    /// 
    /// An interface dealing with the retrieval of grades from the DB
    /// </summary>
    public interface IGradeManager
    {
        string[] RetrieveGradeList();
    }
}
