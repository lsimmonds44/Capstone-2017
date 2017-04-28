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
    /// Created: 2017/03/22
    /// 
    /// Class to handle database interactions involving supplier invoices.
    /// </summary>
    public class SupplierInvoiceAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created: 2017/03/22
        /// 
        /// Retrieves a list of all supplier invoices in the database
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>List of all supplier invoices in the database.</returns>
        public static List<SupplierInvoice> RetrieveSupplierInvoices()
        {
            var supplierInvoices = new List<SupplierInvoice>();

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
                        supplierInvoices.Add(new SupplierInvoice
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

            return supplierInvoices;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/03/22
        /// 
        /// Gets a list of invoice lines for the given invoice id
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="invoiceId">Id of the given invoice.</param>
        /// <returns>The lines of the relevant invoice.</returns>
        public static List<SupplierInvoiceLine> RetrieveInvoiceLinesByInvoiceId(int invoiceId)
        {
            var supplierInvoiceLines = new List<SupplierInvoiceLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_invoice_lines_by_supplier_invoice_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_INVOICE_ID", invoiceId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplierInvoiceLines.Add(new SupplierInvoiceLine
                        {
                            SupplierInvoiceId = reader.GetInt32(0),
                            ProductLotId = reader.GetInt32(1),
                            QuantitySold = reader.GetInt32(2),
                            PriceEach = reader.GetDecimal(3),
                            ItemDiscount = reader.GetDecimal(4),
                            ItemTotal = reader.GetDecimal(5)
                        });
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

            return supplierInvoiceLines;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/03/23
        /// 
        /// Tries to make a connection to approve the invoice associated with the given id
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="invoiceId">The id of the relevant invoice.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdateApproveSupplierInvoice(int invoiceId)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_approve_supplier_invoice_by_supplier_invoice_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_INVOICE_ID", invoiceId);

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
        /// Created: 2017/03/29
        /// 
        /// Stores the invoice to the db and returns the id it was stored to
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="supplierInvoice">The supplierInvoice to create.</param>
        /// <returns>The newly created id.</returns>
        public static int CreateSupplierInvoice(SupplierInvoice supplierInvoice)
        {
            int supplierInvoiceID = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_invoice";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierInvoice.SupplierId);
            cmd.Parameters.AddWithValue("@INVOICE_DATE", supplierInvoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@SUB_TOTAL", supplierInvoice.SubTotal);
            cmd.Parameters.AddWithValue("@TAX_AMOUNT", supplierInvoice.TaxAmount);
            cmd.Parameters.AddWithValue("@TOTAL", supplierInvoice.Total);
            cmd.Parameters.AddWithValue("@AMOUNT_PAID", supplierInvoice.AmountPaid);

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

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/03/29
        /// 
        /// Creates a line for a supplier invoice
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="supplierInvoiceLine">The supplierInvoiceLine to create.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateSupplierInvoiceLine(SupplierInvoiceLine supplierInvoiceLine)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_invoice_line";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_INVOICE_ID", supplierInvoiceLine.SupplierInvoiceId);
            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", supplierInvoiceLine.ProductLotId);
            cmd.Parameters.AddWithValue("@QUANTITY_SOLD", supplierInvoiceLine.QuantitySold);
            cmd.Parameters.AddWithValue("@PRICE_EACH", supplierInvoiceLine.PriceEach);
            cmd.Parameters.AddWithValue("@ITEM_DISCOUNT", supplierInvoiceLine.ItemDiscount);
            cmd.Parameters.AddWithValue("@ITEM_TOTAL", supplierInvoiceLine.ItemTotal);

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
        /// Created: 2017/??/??
        /// 
        /// Updates the passed in "old Invoice" to match the passed in "new Invoice"
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="oldInvoice">The invoice as it is in the database.</param>
        /// <param name="newInvoice">The invoice as it should be.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdateSupplierInvoice(SupplierInvoice oldInvoice, SupplierInvoice newInvoice)
        {
            int rows = 0;

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
        /// Victor Algarin
        /// Created: 2017/04/06
        /// Deletes the supplier invoice by the selected supplier invoice ID
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="invoice">The invoice to be deleted</param>
        /// <returns>Rows affected.</returns>
        public static int DeleteSupplierInvoice(SupplierInvoice invoice)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_supplier_invoice";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_INVOICE_ID", invoice.SupplierInvoiceId);

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
        /// Bobby Thorne
        /// Created: 2017/04/14
        /// 
        /// Retrieve the invoices by the supplier Id
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="supplierID">The id of the relevant supplier.</param>
        /// <returns>List of the supplier invoices</returns>
        public static List<SupplierInvoice> RetrieveSupplierInvoicesBySupplierID(int supplierID)
        {
            var supplierInvoices = new List<SupplierInvoice>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_invoice_list_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplierInvoices.Add(new SupplierInvoice
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

            return supplierInvoices;
        }
    }
}
