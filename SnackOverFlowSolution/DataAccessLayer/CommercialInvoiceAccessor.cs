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
        /// 
        /// Created:
        /// 2017/04/19
        /// 
        /// Retrieve the invoices by the user name associated with a commercial account
        /// </summary>
        /// <param name="userName"></param>
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
                        CommercialInvoice commercialInvoice = new CommercialInvoice
                        {
                            CommercialInvoiceId = reader.GetInt32(0),
                            CommercialId = reader.GetInt32(1),
                            InvoiceDate = reader.GetDateTime(2),
                            SubTotal = reader.GetDecimal(3),
                            TaxAmount = reader.GetDecimal(4),
                            Total = reader.GetDecimal(5),
                            AmountPaid = reader.GetDecimal(6),
                            Approved = reader.GetBoolean(7),
                            Active = reader.GetBoolean(8),
                            OrderId = reader.GetInt32(9)
                        };
                        invoices.Add(commercialInvoice);
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

        /// <summary>
        /// William Flood
        /// 
        /// Created:
        /// 2017/04/20
        /// 
        /// Retrieve the invoices by the user name associated with a commercial account
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List of the supplier invoices</returns>
        public static CommercialInvoice RetrieveCommercialInvoicesByInvoiceID(int invoiceId)
        {
            CommercialInvoice invoice = new CommercialInvoice();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_commercial_invoice_list_by_commercial_invoice_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Commercial_Invoice_Id", invoiceId);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        invoice = new CommercialInvoice
                        {
                            CommercialInvoiceId = reader.GetInt32(0),
                            CommercialId = reader.GetInt32(1),
                            InvoiceDate = reader.GetDateTime(2),
                            SubTotal = reader.GetDecimal(3),
                            TaxAmount = reader.GetDecimal(4),
                            Total = reader.GetDecimal(5),
                            AmountPaid = reader.GetDecimal(6),
                            Approved = reader.GetBoolean(7),
                            Active = reader.GetBoolean(8),
                            OrderId = reader.GetInt32(9)
                        };
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


            return invoice;
        }

        /// <summary>
        /// William Flood
        /// 
        /// Created:
        /// 2017/03/22
        /// 
        /// Gets a list of invoice lines for the given invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public static List<CommercialInvoiceLine> RetrieveInvoiceLinesByInvoiceId(int invoiceId)
        {
            List<CommercialInvoiceLine> lines = new List<CommercialInvoiceLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_commercial_invoice_lines_by_commercial_invoice_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@COMMERCIAL_INVOICE_ID", SqlDbType.Int);
            cmd.Parameters["@COMMERCIAL_INVOICE_ID"].Value = invoiceId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CommercialInvoiceLine line = new CommercialInvoiceLine
                        {
                            CommercialInvoiceId = reader.GetInt32(0),
                            ProductLotId = reader.GetInt32(1),
                            QuantitySold = reader.GetInt32(2),
                            PriceEach = reader.GetDecimal(3),
                            ItemDiscount = reader.GetDecimal(4),
                            ItemTotal = reader.GetInt32(2) * (reader.GetDecimal(3) - reader.GetDecimal(4)),
                            ProductName = reader.GetString(5)
                        };
                        lines.Add(line);
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

            return lines;
        }


        /// <summary>
        /// William Flood
        /// Created 2017/04/30
        /// 
        /// <param name="supplierInvoice">The supplierInvoice to create.</param>
        /// <returns>The newly created id.</returns>
        public static int CreateCustomerInvoice(int orderID, decimal taxRate)
        {
            int supplierInvoiceID = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_commercial_invoice";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TAX_RATE", taxRate);
            cmd.Parameters.AddWithValue("@ORDER_ID", orderID);

            try
            {
                conn.Open();
                int.TryParse(cmd.ExecuteScalar().ToString(), out supplierInvoiceID);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return supplierInvoiceID;
        }

        
    }
}
