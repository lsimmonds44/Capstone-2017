using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using System.Data.SqlClient;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created
    /// 2017/03/08
    /// 
    /// Handles the business logic for Agreements
    /// </summary>
    public class AgreementManager : IAgreementManager
    {
        /// <summary>
        /// Christian Lopez
        /// Created
        /// 2017/03/08
        /// 
        /// Retrieves a list of Agreements for a given supplier Id
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public List<Agreement> RetrieveAgreementsBySupplierId(int supplierId)
        {
            List<Agreement> agreements = new List<Agreement>();

            try
            {
                agreements = AgreementAccessor.retrieveAgreementsBySupplierId(supplierId);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return agreements;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 
        /// 2017/03/08
        /// 
        /// Creates an agreement for the supplier for the given product
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/06
        /// 
        /// Modified method to be less redundant and to change default approverId from 0 to null.
        /// approverId was changed to be nullable as well.
        /// </remarks>
        /// <param name="supplier"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool CreateAgreementsForSupplier(Supplier supplier, Product product, int? approverId = null, bool isApproved = true)
        {
            bool success = false;
            var agreement = new Agreement()
            {
                ProductId = product.ProductId,
                SupplierId = supplier.SupplierID,
                DateSubmitted = DateTime.Now,
                IsApproved = isApproved,
                ApprovedBy = approverId
            };
            if (approverId == null)
            {
                agreement.IsApproved = false;
            }
            try
            {
                if (1 == AgreementAccessor.CreateAgreement(agreement))
                {
                    success = true;
                }

            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
            return success;
        }


        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/03/09
        /// 
        /// Deactivates a specified agreement.
        /// </summary>
        /// <param name="agreement"></param>
        /// <param name="approverId"></param>
        /// <returns></returns>
        public bool UpdateAgreement(Agreement oldAgreement, Agreement newAgreement)
        {
            bool success = false;
            try
            {
                if (1 == AgreementAccessor.UpdateAgreement(oldAgreement, newAgreement))
                {
                    success = true;
                }
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
            return success;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/03/09
        /// 
        /// Creates an Agreement based on the inputs.
        /// </summary>
        /// <param name="agreementId"></param>
        /// <param name="productId"></param>
        /// <param name="supplierId"></param>
        /// <param name="dateSubmitted"></param>
        /// <param name="isApproved"></param>
        /// <param name="isActive"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public Agreement MakeAgreement(int agreementId, int productId, int supplierId, DateTime dateSubmitted, bool isApproved, bool isActive, int? approvedBy = null)
        {
            return new Agreement()
            {
                AgreementId = agreementId,
                ProductId = productId,
                SupplierId = supplierId,
                DateSubmitted = dateSubmitted,
                IsApproved = isApproved,
                Active = isActive,
                ApprovedBy = approvedBy
            };
        }
    }
}
