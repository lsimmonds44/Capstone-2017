using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created 2017/03/08
    /// 
    /// Handles the business logic for Agreements
    /// </summary>
    public class AgreementManager : IAgreementManager
    {
        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Retrieves a list of Agreements for a given supplier Id
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public List<Agreement> RetrieveAgreementsBySupplierId(int supplierId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Creates an agreement for the supplier for the given product
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool CreateAgreementsForSupplier(Supplier supplier, Product product, int approverId = 0, bool isApproved = true)
        {
            bool success = false;
            try
            {
                if (0 != approverId) //We have an approver
                {
                    if (1 == AgreementAccessor.CreateAgreement(product.ProductId, supplier.SupplierID, DateTime.Now, isApproved, approverId))
                    {
                        success = true;
                    }
                }
                else // Making an application, not approving
                {
                    if (1 == AgreementAccessor.CreateAgreementApplication(product.ProductId, supplier.SupplierID, DateTime.Now))
                    {
                        success = true;
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return success;
        }

    }
}
