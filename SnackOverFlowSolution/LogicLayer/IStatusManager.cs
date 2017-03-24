using System.Collections.Generic;

namespace LogicLayer
{
    /// <summary>
    /// Aaron Usher
    /// Created: 2017/03/24
    /// 
    /// Interface to represent a status manager.
    /// </summary>
    public interface IStatusManager
    {
        List<string> RetrieveStatusList();
    }
}