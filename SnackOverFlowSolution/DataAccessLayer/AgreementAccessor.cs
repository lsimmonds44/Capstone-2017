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
                            IsApproved = reader.GetBoolean(4)
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
    }
}
