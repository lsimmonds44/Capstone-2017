///<summary>
///Robert Forbes
///2017/02/07
///
///</summary>
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IPackageLineManager
    {
        bool CreatePackageLine(PackageLine line);

        List<PackageLine> RetrievePackageLinesInPackage(int packageID);
    }
}
