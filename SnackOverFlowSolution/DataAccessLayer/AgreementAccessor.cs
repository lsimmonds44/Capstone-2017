using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created:
    /// 2017/03/08
    /// 
    /// Handles accessing a DB for data about Agreements
    /// </summary>
    public class AgreementAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created: 
        /// 2017/03/08
        /// 
        /// Retrieves a list of Agreements for a supplier from the DB
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="supplierId">The supplier id to search on.</param>
        /// <returns>A list of agreements associated with the given supplier Id.</returns>
        public static List<Agreement> retrieveAgreementsBySupplierId(int supplierId)
        {
            List<Agreement> agreements = new List<Agreement>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_agreement_list_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        agreements.Add(new Agreement()
                        {
                            AgreementId = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            SupplierId = reader.GetInt32(2),
                            DateSubmitted = reader.GetDateTime(3),
                            IsApproved = reader.GetBoolean(4),
                            ApprovedBy = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            Active = reader.GetBoolean(6)
                        });
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }
            

            return agreements;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 
        /// 2017/03/08
        /// 
        /// Attempts to store an  Agreement to the DB
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/06
        ///
        /// Standardized method.
        /// </remarks>
        /// 
        /// Ariel Sigo
        /// Christian Lopez
        /// Updated 
        /// 2017/04/07
        /// 
        /// Fixed crashing errors
        /// 
        /// <param name="agreement">The agreement to store.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateAgreement(Agreement agreement)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            string cmdText;
            if (agreement.ApprovedBy == null)
            {
                cmdText = @"sp_create_agreement_application";
            }
            else
            {
                cmdText = @"sp_create_agreement";
            }

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", agreement.ProductId);
            cmd.Parameters.AddWithValue("@SUPPLIER_ID", agreement.SupplierId);
            cmd.Parameters.AddWithValue("@DATE_SUBMITTED", agreement.DateSubmitted);
            cmd.Parameters.AddWithValue("@IS_APPROVED", agreement.IsApproved);
            if (agreement.ApprovedBy != null)
            {
                cmd.Parameters.AddWithValue("@APPROVED_BY", agreement.ApprovedBy);
            }
            

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 
        /// 2017/03/09
        /// 
        /// Attempts to update an existing agreement in the DB
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Created: 
        /// 2017/04/06
        /// 
        /// Standardized method.
        /// 
        /// </remarks>
        /// 
        /// <param name="oldAgreement">The agreement as it is in the database, for concurrency checks.</param>
        /// <param name="newAgreement">The agreement as it should be.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdateAgreement(Agreement oldAgreement, Agreement newAgreement)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_agreement";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@AGREEMENT_ID", oldAgreement.AgreementId);
            cmd.Parameters.AddWithValue("@old_PRODUCT_ID", oldAgreement.ProductId);
            cmd.Parameters.AddWithValue("@old_SUPPLIER_ID", oldAgreement.SupplierId);
            cmd.Parameters.AddWithValue("@old_DATE_SUBMITTED", oldAgreement.DateSubmitted);
            cmd.Parameters.AddWithValue("@old_IS_APPROVED", oldAgreement.IsApproved);
            cmd.Parameters.AddWithValue("@old_APPROVED_BY", oldAgreement.ApprovedBy);
            cmd.Parameters.AddWithValue("@old_ACTIVE", oldAgreement.Active);

            cmd.Parameters.AddWithValue("@new_PRODUCT_ID", newAgreement.ProductId);
            cmd.Parameters.AddWithValue("@new_SUPPLIER_ID", newAgreement.SupplierId);
            cmd.Parameters.AddWithValue("@new_DATE_SUBMITTED", newAgreement.DateSubmitted);
            cmd.Parameters.AddWithValue("@new_IS_APPROVED", newAgreement.IsApproved);
            cmd.Parameters.AddWithValue("@new_APPROVED_BY", newAgreement.ApprovedBy);
            cmd.Parameters.AddWithValue("@new_ACTIVE", newAgreement.Active);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/06
        /// 
        /// Returns a list of agreements with the product names
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static List<AgreementWithProductName> RetrieveAgreementsWithProductNameBySupplierId(int supplierId)
        {
            List<AgreementWithProductName> agreementsWithNames = new List<AgreementWithProductName>();

            try
            {
                List<Agreement> agreementsBySupplier = retrieveAgreementsBySupplierId(supplierId);
                foreach (Agreement a in agreementsBySupplier)
                {
                    AgreementWithProductName newAgreement = new AgreementWithProductName()
                    {
                        ProductId = a.ProductId,
                        AgreementId = a.AgreementId,
                        Active = a.Active,
                        ApprovedBy = a.ApprovedBy,
                        IsApproved = a.IsApproved,
                        DateSubmitted = a.DateSubmitted,
                        SupplierId = a.SupplierId,
                        ProductName = ProductAccessor.RetrieveProduct(a.ProductId).Name
                    };
                    agreementsWithNames.Add(newAgreement);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return agreementsWithNames;
        }

    }
}
