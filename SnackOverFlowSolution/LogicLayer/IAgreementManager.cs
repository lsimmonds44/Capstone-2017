using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IAgreementManager
    {
        List<Agreement> RetrieveAgreementsBySupplierId(int supplierId);

        bool CreateAgreementsForSupplier(Supplier supplier, Product product, int approverID = 0, bool isApproved = true);

        bool UpdateAgreement(Agreement oldAgreement, Agreement newAgreement);

        Agreement MakeAgreement(int agreementId, int productId, int supplierId, DateTime dateSubmitted, bool isApproved, 
            bool isActive, int? approvedBy = null);


    }
}
