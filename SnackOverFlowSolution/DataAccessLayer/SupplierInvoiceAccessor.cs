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
    /// 2017/03/22
    /// 
    /// Contains the information to connect to the DB regarding Supplier Invoices and their Lines
    /// </summary>
    public class SupplierInvoiceAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Retrieves a list of all supplier invoices in the database
        /// </summary>
        /// <returns></returns>
        public static List<SupplierInvoice> RetrieveSupplierInvoices()
        {
            List<SupplierInvoice> invoices = new List<SupplierInvoice>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_invoice_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierInvoice supplierInvoice = new SupplierInvoice
                        {
                            SupplierInvoiceId = reader.GetInt32(0),
                            SupplierId = reader.GetInt32(1),
                            InvoiceDate = reader.GetDateTime(2),
                            SubTotal = reader.GetDecimal(3),
                            TaxAmount = reader.GetDecimal(4),
                            Total = reader.GetDecimal(5),
                            AmountPaid = reader.GetDecimal(6),
                            Active = reader.GetBoolean(7)
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

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Gets a list of invoice lines for the given invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public static List<SupplierInvoiceLine> RetrieveInvoiceLinesByInvoiceId(int invoiceId)
        {
            List<SupplierInvoiceLine> lines = new List<SupplierInvoiceLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_invoice_lines_by_supplier_invoice_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SUPPLIER_INVOICE_ID", SqlDbType.Int);
            cmd.Parameters["@SUPPLIER_INVOICE_ID"].Value = invoiceId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierInvoiceLine line = new SupplierInvoiceLine
                        {
                            SupplierInvoiceId = reader.GetInt32(0),
                            ProductLotId = reader.GetInt32(1),
                            QuantitySold = reader.GetInt32(2),
                            PriceEach = reader.GetDecimal(3),
                            ItemDiscount = reader.GetDecimal(4),
                            ItemTotal = reader.GetDecimal(5)
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

    }
}
