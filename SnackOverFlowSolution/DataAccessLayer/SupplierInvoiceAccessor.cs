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

        /// <summary>
        /// Christian Lopez
        /// 2017/03/23
        /// 
        /// Tries to make a connection to approve the invoice associated with the given id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public static int UpdateApproveSupplierInvoice(int invoiceId)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_approve_supplier_invoice_by_supplier_invoice_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SUPPLIER_INVOICE_ID", SqlDbType.Int);
            cmd.Parameters["@SUPPLIER_INVOICE_ID"].Value = invoiceId;

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
        /// 2017/03/29
        /// 
        /// Stores the invoice to the db and returns the id it was stored to
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static int CreateSupplierInvoice(SupplierInvoice invoice)
        {
            int id = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_invoice";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", invoice.SupplierId);
            cmd.Parameters.AddWithValue("@INVOICE_DATE", invoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@SUB_TOTAL", invoice.SubTotal);
            cmd.Parameters.AddWithValue("@TAX_AMOUNT", invoice.TaxAmount);
            cmd.Parameters.AddWithValue("@TOTAL", invoice.Total);
            cmd.Parameters.AddWithValue("@AMOUNT_PAID", invoice.AmountPaid);

            try
            {
                conn.Open();
                int.TryParse(cmd.ExecuteScalar().ToString(), out id);
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }

            return id;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Creates a line for a supplier invoice
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int CreateSupplierInvoiceLine(SupplierInvoiceLine line)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_invoice_line";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_INVOICE_ID", line.SupplierInvoiceId);
            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", line.ProductLotId);
            cmd.Parameters.AddWithValue("@QUANTITY_SOLD", line.QuantitySold);
            cmd.Parameters.AddWithValue("@PRICE_EACH", line.PriceEach);
            cmd.Parameters.AddWithValue("@ITEM_DISCOUNT", line.ItemDiscount);
            cmd.Parameters.AddWithValue("@ITEM_TOTAL", line.ItemTotal);

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
        /// Robert Forbes
        /// Updates the passed in "old Invoice" to match the passed in "new Invoice"
        /// </summary>
        /// <param name="oldInvoice">The invoice as it was before editing</param>
        /// <param name="newInvoice">The invoice after it has been edited</param>
        /// <returns></returns>
        public static int UpdateSupplierInvoice(SupplierInvoice oldInvoice, SupplierInvoice newInvoice)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_supplier_invoice";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_INVOICE_ID", oldInvoice.SupplierInvoiceId);
            cmd.Parameters.AddWithValue("@old_SUPPLIER_ID", oldInvoice.SupplierId);
            cmd.Parameters.AddWithValue("@old_INVOICE_DATE", oldInvoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@old_SUB_TOTAL", oldInvoice.SubTotal);
            cmd.Parameters.AddWithValue("@old_TAX_AMOUNT", oldInvoice.TaxAmount);
            cmd.Parameters.AddWithValue("@old_TOTAL", oldInvoice.Total);
            cmd.Parameters.AddWithValue("@old_AMOUNT_PAID", oldInvoice.AmountPaid);
            cmd.Parameters.AddWithValue("@old_APPROVED", oldInvoice.Approved);
            cmd.Parameters.AddWithValue("@old_ACTIVE", oldInvoice.Active);


            cmd.Parameters.AddWithValue("@new_SUPPLIER_ID", newInvoice.SupplierId);
            cmd.Parameters.AddWithValue("@new_INVOICE_DATE", newInvoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@new_SUB_TOTAL", newInvoice.SubTotal);
            cmd.Parameters.AddWithValue("@new_TAX_AMOUNT", newInvoice.TaxAmount);
            cmd.Parameters.AddWithValue("@new_TOTAL", newInvoice.Total);
            cmd.Parameters.AddWithValue("@new_AMOUNT_PAID", newInvoice.AmountPaid);
            cmd.Parameters.AddWithValue("@new_APPROVED", newInvoice.Approved);
            cmd.Parameters.AddWithValue("@new_ACTIVE", newInvoice.Active);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

    }

}
