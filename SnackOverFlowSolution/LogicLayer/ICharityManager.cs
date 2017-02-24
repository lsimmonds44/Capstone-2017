using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ICharityManager
    {
        List<Charity> RetrieveCharityList();
        
        int AddCharity(Charity charityInstance);
    }
}
