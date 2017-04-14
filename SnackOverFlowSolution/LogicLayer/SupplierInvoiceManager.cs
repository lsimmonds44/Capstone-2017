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
    /// 2017/03/22
    /// </summary>
    public class SupplierInvoiceManager : ISupplierInvoiceManager
    {

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Retrieves a list of all supplier invoices, regardless of status
        /// </summary>
        /// <returns></returns>
        public List<SupplierInvoice> RetrieveAllSupplierInvoices()
        {
            try
            {
                return SupplierInvoiceAccessor.RetrieveSupplierInvoices();
            }
            catch (SqlException ex)
            {
                
                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Attempts to retrieve a list of invoice lines by the given id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public List<SupplierInvoiceLine> RetrieveSupplierInvoiceLinesByInvoiceId(int invoiceId)
        {
            try
            {
                return SupplierInvoiceAccessor.RetrieveInvoiceLinesByInvoiceId(invoiceId);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/23
        /// 
        /// Attempts to approve the invoice with the given invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public bool ApproveSupplierInvoice(int invoiceId)
        {
            bool result = false;

            try
            {
                if (1 == SupplierInvoiceAccessor.UpdateApproveSupplierInvoice(invoiceId))
                {
                    result = true;
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

            return result;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Creates an invoice and returns the id associated
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int CreateSupplierInvoice(SupplierInvoice invoice)
        {
            int id = 1;
            try
            {
                id = SupplierInvoiceAccessor.CreateSupplierInvoice(invoice);
                if (id == 0)
                {
                    // We were not able to capture the new id
                    throw new ApplicationException("Unable to retrieve the new id number.");
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
            return id;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Adds a supplier invoice line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool CreateSupplierInvoiceLine(SupplierInvoiceLine line)
        {
            try
            {
                return (1 == SupplierInvoiceAccessor.CreateSupplierInvoiceLine(line));
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }



        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Updates the old invoice in the database so that it has the values of the new invoice
        /// </summary>
        /// <param name="oldInvoice">The original invoice</param>
        /// <param name="newInvoice">The invoice with updated values</param>
        /// <returns></returns>
        public bool UpdateSupplierInvoice(SupplierInvoice oldInvoice, SupplierInvoice newInvoice)
        {
            try
            {
                return (1 == SupplierInvoiceAccessor.UpdateSupplierInvoice(oldInvoice, newInvoice));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Victor Algarin
        /// 2017/04/06 
        /// </summary>
        /// 
        /// <param name="invoice">The selected invoice to be deleted</param>
        /// <returns>true (if query from accessor is successful)</returns>
        public bool DeleteSupplierInvoice(SupplierInvoice invoice)
        {
            try
            {
                return (SupplierInvoiceAccessor.DeleteSupplierInvoice(invoice) == 1);
            }
            catch (Exception)
            {
                return false;
                throw new ApplicationException("There was a problem deleting the specified supplier invoice");
            }
        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/04/14
        /// 
        /// Retrieve the invoices by the supplier Id
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of the supplier invoices</returns>
        public List<SupplierInvoice> RetrieveAllSupplierInvoicesBySupplierId(int supplierID)
        {
            try
            {
                return SupplierInvoiceAccessor.RetrieveSupplierInvoicesBySupplierID(supplierID);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }
    }
}
