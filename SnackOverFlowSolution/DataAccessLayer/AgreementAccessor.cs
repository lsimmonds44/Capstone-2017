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
    /// Created 2017/03/08
    /// 
    /// Handles accessing a DB for data about Agreements
    /// </summary>
    public class AgreementAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Retrieves a list of Agreements for a supplier from the DB
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static List<Agreement> retrieveAgreementsBySupplierId(int supplierId)
        {
            List<Agreement> agreements = new List<Agreement>();


            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_agreement_list_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters["@SUPPLIER_ID"].Value = supplierId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Agreement a = new Agreement()
                        {
                            AgreementId = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            SupplierId = reader.GetInt32(2),
                            DateSubmitted = reader.GetDateTime(3),
                            IsApproved = reader.GetBoolean(4),
                            Active = reader.GetBoolean(6)
                        };
                        if (!reader.IsDBNull(5))
                        {
                            a.ApprovedBy = reader.GetInt32(5);
                        }
                        else
                        {
                            a.ApprovedBy = null;
                        }

                        agreements.Add(a);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            

            return agreements;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Attempts to store an Approved Agreement to the DB
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="supplierId"></param>
        /// <param name="dateSubmitted"></param>
        /// <param name="isApproved"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public static int CreateAgreement(int productId, int supplierId, DateTime dateSubmitted, bool isApproved, int approvedBy)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_agreement";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@DATE_SUBMITTED", SqlDbType.DateTime);
            cmd.Parameters.Add("@IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@APPROVED_BY", SqlDbType.Int);

            cmd.Parameters["@PRODUCT_ID"].Value = productId;
            cmd.Parameters["@SUPPLIER_ID"].Value = supplierId;
            cmd.Parameters["@DATE_SUBMITTED"].Value = dateSubmitted;
            cmd.Parameters["@IS_APPROVED"].Value = isApproved;
            cmd.Parameters["@APPROVED_BY"].Value = approvedBy;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Creates an un-approved agreement to the DB
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="supplierId"></param>
        /// <param name="dateSubmitted"></param>
        /// <returns></returns>
        public static int CreateAgreementApplication(int productId, int supplierId, DateTime dateSubmitted)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_agreement_application";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@DATE_SUBMITTED", SqlDbType.DateTime);
            cmd.Parameters.Add("@IS_APPROVED", SqlDbType.Bit);

            cmd.Parameters["@PRODUCT_ID"].Value = productId;
            cmd.Parameters["@SUPPLIER_ID"].Value = supplierId;
            cmd.Parameters["@DATE_SUBMITTED"].Value = dateSubmitted;
            cmd.Parameters["@IS_APPROVED"].Value = false;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/09
        /// 
        /// Attempts to update an existing agreement in the DB
        /// </summary>
        /// <param name="oldAgreement"></param>
        /// <param name="newAgreement"></param>
        /// <returns></returns>
        public static int UpdateAgreement(Agreement oldAgreement, Agreement newAgreement)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_agreement";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@old_AGREEMENT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_DATE_SUBMITTED", SqlDbType.DateTime);
            cmd.Parameters.Add("@new_DATE_SUBMITTED", SqlDbType.DateTime);
            cmd.Parameters.Add("@old_IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@new_IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@old_APPROVED_BY", SqlDbType.Int);
            cmd.Parameters.Add("@new_APPROVED_BY", SqlDbType.Int);
            cmd.Parameters.Add("@old_ACTIVE", SqlDbType.Bit);
            cmd.Parameters.Add("@new_ACTIVE", SqlDbType.Bit);

            cmd.Parameters["@old_AGREEMENT_ID"].Value = oldAgreement.AgreementId;
            cmd.Parameters["@old_PRODUCT_ID"].Value = oldAgreement.ProductId;
            cmd.Parameters["@new_PRODUCT_ID"].Value = newAgreement.ProductId;
            cmd.Parameters["@old_SUPPLIER_ID"].Value = oldAgreement.SupplierId;
            cmd.Parameters["@new_SUPPLIER_ID"].Value = newAgreement.SupplierId;
            cmd.Parameters["@old_DATE_SUBMITTED"].Value = oldAgreement.DateSubmitted;
            cmd.Parameters["@new_DATE_SUBMITTED"].Value = newAgreement.DateSubmitted;
            cmd.Parameters["@old_IS_APPROVED"].Value = oldAgreement.IsApproved;
            cmd.Parameters["@new_IS_APPROVED"].Value = newAgreement.IsApproved;
            cmd.Parameters["@old_APPROVED_BY"].Value = oldAgreement.ApprovedBy;
            cmd.Parameters["@new_APPROVED_BY"].Value = newAgreement.ApprovedBy;
            cmd.Parameters["@old_ACTIVE"].Value = oldAgreement.Active;
            cmd.Parameters["@new_ACTIVE"].Value = newAgreement.Active;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
