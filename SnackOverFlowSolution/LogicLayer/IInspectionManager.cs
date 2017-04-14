using System;
using DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/16
    /// 
    /// The interface for the Inspection Managers
    /// </summary>
    public interface IInspectionManager
    {
        bool CreateInspection(Inspection inspection);
    }
}
