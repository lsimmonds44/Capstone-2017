using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CommercialInvoiceAccessor
    {

        /// <summary>
        /// William Flood
        /// 2017/04/19
        /// 
        /// Retrieve the invoices by the user name associated with a commercial account
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of the supplier invoices</returns>
        public static List<CommercialInvoice> RetrieveCommercialInvoicesByUserName(string userName)
        {
            List<CommercialInvoice> invoices = new List<CommercialInvoice>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_commercial_invoice_list_by_user_name";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@USER_NAME", userName);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CommercialInvoice supplierInvoice = new CommercialInvoice
                        {
                            CommercialInvoiceId = reader.GetInt32(0),
                            CommercialId = reader.GetInt32(1),
                            InvoiceDate = reader.GetDateTime(2),
                            SubTotal = reader.GetDecimal(3),
                            TaxAmount = reader.GetDecimal(4),
                            Total = reader.GetDecimal(5),
                            AmountPaid = reader.GetDecimal(6),
                            Approved = reader.GetBoolean(7),
                            Active = reader.GetBoolean(8)
                        };
                        invoices.Add(supplierInvoice);
                    }
                }

                reader.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }


            return invoices;
        }
        
    }
}
