using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Aaron Usher
    /// Created: 2017/03/24
    /// 
    /// Interface to represent a delivery type manager.
    /// </summary>
    public interface IDeliveryTypeManager
    {
        List<string> RetrieveDeliveryTypeList();
    }
}
